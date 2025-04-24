using _00.Work.MOON._01.Script.Entities;
using _00.Work.MOON._01.Script.SO.Player;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Players
{
    public class PlayerMovement : EntityMovement
    {
        [SerializeField] private Transform rotateTarget; // 회전 대상
        public PlayerInputSO PlayerInput { get; private set; }
        private PlayerMoveStatInfoSO _statInfo;

        // 이동 관련 변수들
        protected float _moveThresholdTime;
        private bool _isMoveCheck = false;
        private float _timer;
        private Vector3 _movementDirection;
        
        public bool IsCanMove { get; private set; } = false;

        private float RotateTargetY => rotateTarget.eulerAngles.y;
        private Transform parent;

        protected void Awake()
        {
            GetMoveStatsInfo(); // 이동 관련 스탯 정보 초기화
            rb.constraints = RigidbodyConstraints.FreezeRotation; // Rigidbody 회전 고정
        }

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            parent = entity.transform;
            PlayerInput.IsMoveThreshold += IsMoveChecking; // MoveThreshold 이벤트 연결
        }

        public void OnDestroy()
        {
            PlayerInput.IsMoveThreshold -= IsMoveChecking; // 이벤트 연결 해제
        }

        private void IsMoveChecking(bool isMove)
        {
            _isMoveCheck = isMove; // 이동 가능 여부 업데이트
        }

        private void GetMoveStatsInfo()
        {
            _statInfo = statInfo as PlayerMoveStatInfoSO; // 이동 스탯 정보 캐스팅
            PlayerInput = _statInfo.PlayerInput;
            _moveThresholdTime = _statInfo.MoveThresholdTime;
        }

        public void SetMovementDirection(Vector2 movementInput, float movementProportion = 1f)
        {
            // 입력된 이동 방향을 정규화하고 이동 비율 적용
            _movementDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized * movementProportion;
        }


        protected override void FixedUpdate()
        {
            if (IsCanMove)
            {
                //CalculateMovement(); // 이동 계산
                CalculateMovement();
            }
            else
            {
                Stop(); // 이동 중지
            }
            SlopeMove();
            Move(); // Rigidbody를 이용해 이동
            MoveMaxCheck(); // 이동량 최대치 확인
        }

        private void Update()
        {
            // 이동 가능 여부를 타이머 기반으로 체크
            if (_isMoveCheck)
            {
                if (_timer <= 0)
                {
                    IsCanMove = true;
                }
                else
                {
                    IsCanMove = false;
                    _timer -= Time.deltaTime;
                }
            }
            else
            {
                _timer = _moveThresholdTime;
                IsCanMove = false;
            }
        }

        public void Stop()
        {
            // 이동 중지 로직: 속도를 서서히 감소
            _velocity = new Vector3(_velocity.x, 0, _velocity.z);
            rb.AddForce(-rb.linearVelocity * stopPer, ForceMode.Impulse);

            if (Mathf.Abs(Vector3.Magnitude(new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z))) < stopDistance)
            {
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            }
        }
    
        private void CalculateMovement()
        {
            // 기본 이동 벡터 계산 (평면 이동 로직)
            _velocity = Quaternion.Euler(0, RotateTargetY, 0) * _movementDirection;
            _velocity *= _moveSpeed * Time.fixedDeltaTime * 100;

            // 회전 처리: 이동 중일 때 캐릭터 회전
            if (_velocity.magnitude > 0)
            {
                Vector3 currentEulerAngles = parent.rotation.eulerAngles;
                float targetYAngle = Quaternion.LookRotation(_velocity).eulerAngles.y;

                // 부드럽게 회전
                float changedYAngle =
                    Mathf.LerpAngle(currentEulerAngles.y, targetYAngle, Time.fixedDeltaTime * _rotateSpeed);
                parent.rotation = Quaternion.Euler(currentEulerAngles.x, changedYAngle, currentEulerAngles.z);
            }

            _velocity.y = 0;
        }

        private void SlopeMove()
        {
            // 경사면에 닿았는지 확인
            if (_groundChecker.GroundCheck(out RaycastHit hit))
            {
                rb.useGravity = false;
                // 충돌한 지점 계산
                Vector3 pos = hit.point + hit.normal * _upDistance;

                // 경사면 노멀을 기반으로 회전 계산 (Z축 고정)
                Vector3 slopeForward = Vector3.ProjectOnPlane(parent.forward, hit.normal).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(slopeForward, hit.normal);

                // 현재 위치를 경사면에 맞춰 이동
                _slopeVelocity = (pos - parent.transform.position) * (Time.fixedDeltaTime * 100f * _moveToUPSpeed);

                // 부드럽게 회전
                parent.rotation = Quaternion.Lerp(parent.rotation, targetRotation, Time.fixedDeltaTime * _rotateSpeed);
            }
            else
            {
                rb.useGravity = true;
            }
        }






        protected override void Move()
        {
            // Rigidbody를 이용한 이동
            rb.AddForce(_slopeVelocity, ForceMode.Force);
            rb.AddForce(_velocity, ForceMode.Force);
        }
    }
}

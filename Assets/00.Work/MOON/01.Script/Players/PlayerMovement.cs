using System.Collections.Generic;
using _00.Work.MOON._01.Script.Entities;
using _00.Work.MOON._01.Script.SO.Entity;
using _00.Work.MOON._01.Script.SO.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _00.Work.MOON._01.Script.Players
{
    public class PlayerMovement : EntityMovement
    {
        #region Variables

        [SerializeField] private Transform rotateTarget; // 회전 대상

        private PlayerInputSO _playerInput;
        private PlayerMoveStatInfoSO _statInfo;
        private Rigidbody _rb; // Rigidbody 참조
        private ChangeModel _changeModel;

        // 이동 관련 변수들   
        private float _moveThresholdTime;
        private bool _isMoveCheck = false;
        private float _timer;
        private Vector3 _movementDirection;

        // MoveStat에서 받는 스탯
        private float _upDistance;
        private float _jumpPower;
        private float _maxSpeed;
        private float _stopPer;
        private float _moveToUpSpeed;

        // 현재 플레이어 이동 스탯
        private PlayerMoveStatSO CurrentPlayerMoveStat => (PlayerMoveStatSO)_currentMoveStat;

        // 이동 가능 여부
        public bool IsCanMove { get; private set; } = false;

        // 회전 대상의 Y축 회전 값
        private float RotateTargetY => rotateTarget.eulerAngles.y;

        #endregion

        #region Initialization

        public override void Initialize(Entity entity)
        {
            GetMoveStatsInfo(); // 이동 관련 스탯 정보 초기화
            Player player = entity as Player;
            _rb = player.Rb;
            _rb.constraints = RigidbodyConstraints.FreezeRotation; // Rigidbody 회전 고정
            _changeModel = entity.GetCompo<ChangeModel>();
            SetModel(); // 모델 세팅
            base.Initialize(entity);
            _playerInput.IsMoveThreshold += IsMoveChecking; // MoveThreshold 이벤트 연결
        }

        public void OnDestroy()
        {
            _playerInput.IsMoveThreshold -= IsMoveChecking; // 이벤트 연결 해제
        }

        /// <summary>
        /// 이동 가능 여부를 업데이트하는 메서드
        /// </summary>
        /// <param name="isMove">이동 가능 여부</param>
        private void IsMoveChecking(bool isMove)
        {
            _isMoveCheck = isMove; // 이동 가능 여부 업데이트
        }

        private void GetMoveStatsInfo()
        {
            _statInfo = statInfo as PlayerMoveStatInfoSO; // 이동 스탯 정보 캐스팅
            _playerInput = _statInfo.PlayerInput;
            _moveThresholdTime = _statInfo.MoveThresholdTime;
        }

        private void SetModel()
        {
            foreach (KeyValuePair<string, EntityMoveStatSO> stat in statInfo.MoveStats)
            {
                PlayerMoveStatSO playerStat = stat.Value as PlayerMoveStatSO;
                _changeModel.AddDictionary(stat.Key, playerStat.CarPrefab);
            }
        }

        #endregion

        #region Stat Management

        public override void ChangeStat(string statType)
        {
            base.ChangeStat(statType);
            _upDistance = CurrentPlayerMoveStat.UpDistance;
            _maxSpeed = CurrentPlayerMoveStat.MaxSpeed;
            _jumpPower = CurrentPlayerMoveStat.JumpPower;
            _stopPer = CurrentPlayerMoveStat.StopPer;
            _moveToUpSpeed = CurrentPlayerMoveStat.MoveToUpSpeed;
            _changeModel.ChangeCarModel(statType);
        }

        #endregion

        #region Movement Logic

        public void SetMovementDirection(Vector2 movementInput, float movementProportion = 1f)
        {
            // 입력된 이동 방향을 정규화하고 이동 비율 적용
            _movementDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized * movementProportion;
        }

        protected override void FixedUpdate()
        {
            if (IsCanMove)
            {
                CalculateMovement(); // 이동 계산
            }
            else if (!_rb.useGravity)
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

        private void CalculateMovement()
        {
            // 기본 이동 벡터 계산 (평면 이동 로직)
            _velocity = Quaternion.Euler(0, RotateTargetY, 0) * _movementDirection;
            _velocity *= _moveSpeed * Time.fixedDeltaTime * 100;

            // 회전 처리: 이동 중일 때 캐릭터 회전
            if (_velocity.magnitude > 0)
            {
                Vector3 currentEulerAngles = _parent.rotation.eulerAngles;
                float targetYAngle = Quaternion.LookRotation(_velocity).eulerAngles.y;

                // 부드럽게 회전
                float changedYAngle =
                    Mathf.LerpAngle(currentEulerAngles.y, targetYAngle, Time.fixedDeltaTime * _rotateSpeed);
                _parent.rotation = Quaternion.Euler(currentEulerAngles.x, changedYAngle, currentEulerAngles.z);
            }

            _velocity.y = 0;
        }
        
        /// <summary>
        /// 경사면 이동 처리
        /// </summary>
        private void SlopeMove()
        {
            // 경사면에 닿았는지 확인
            if (_groundChecker.GroundCheck(out RaycastHit hit))
            {
                _rb.useGravity = false;
                // 충돌한 지점 계산
                Vector3 pos = hit.point + hit.normal * _upDistance;

                // 경사면 노멀을 기반으로 회전 계산 (Z축 고정)
                Vector3 slopeForward = Vector3.ProjectOnPlane(_parent.forward, hit.normal).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(slopeForward, hit.normal);

                if ((pos - _parent.transform.position).magnitude <= 0.15f)
                {
                    _parent.transform.position = pos;
                }
                else
                {
                    // 현재 위치를 경사면에 맞춰 이동
                    _slopeVelocity = (pos - _parent.transform.position) * (Time.fixedDeltaTime * 100f * _moveToUpSpeed);
                }

                // 부드럽게 회전
                _parent.rotation =
                    Quaternion.Lerp(_parent.rotation, targetRotation, Time.fixedDeltaTime * _rotateSpeed);
            }
            else
            {
                _rb.useGravity = true;
            }
        }

        public void Jump()
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse); // 위 방향으로 힘 추가
        }

        public void Stop()
        {
            // 이동 중지 로직: 속도를 서서히 감소
            _velocity = Vector3.zero;
            if (Mathf.Abs(Vector3.Magnitude(new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z))) <=
                _stopDistance)
            {
                _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
            }
            else
            {
                _rb.AddForce(-_rb.linearVelocity * _stopPer, ForceMode.Impulse);
            }
        }

        protected override void Move()
        {
            // Rigidbody를 이용한 이동
            _rb.AddForce(_slopeVelocity, ForceMode.Force);
            _rb.AddForce(_velocity, ForceMode.Force);
        }

        protected void MoveMaxCheck()
        {
            Vector3 tempVelocity = _rb.linearVelocity;
            tempVelocity.y = 0;
            if (tempVelocity.magnitude > _maxSpeed)
            {
                tempVelocity = tempVelocity.normalized * (_maxSpeed + 0.01f); // 최대 속도 제한
                tempVelocity.y = _rb.linearVelocity.y;
                _rb.linearVelocity = tempVelocity;
            }
        }

        #endregion
    }
}

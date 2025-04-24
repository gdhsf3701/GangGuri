using _00.Work.MOON._01.Script.SO.Entity;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Entities
{
    public abstract class EntityMovement : MonoBehaviour, IEntityComponent
    {
        [SerializeField] protected EntityMoveStatInfoSO statInfo; // 이동 스탯 정보
        [SerializeField] protected Rigidbody rb; // Rigidbody 참조

        // 현재 이동 스탯
        protected EntityMoveStatSO _currentMoveStat;

        // 이동 관련 변수
        protected float _moveSpeed;
        protected float _moveToUPSpeed;
        protected float _maxSpeed;
        protected float _jumpPower;
        protected float _rotateSpeed;
        protected float _upDistance;
        protected float _maxSlopeAngle;
        protected float stopPer;
        protected float stopDistance;
        
        protected Vector3 _slopeVelocity;

        protected Vector3 _velocity; // 이동 속도

        protected Entity _entity; // 엔티티 참조
        protected GroundChecker _groundChecker; // 지면 체크

        /// <summary>
        /// 엔티티 초기화
        /// </summary>
        public virtual void Initialize(Entity entity)
        {
            _entity = entity;
            ChangeStat("NORMAL"); // 기본 스탯 설정
            _groundChecker = _entity.GetCompo<GroundChecker>(); // GroundChecker 컴포넌트 참조
        }

        /// <summary>
        /// 이동 스탯 변경
        /// </summary>
        /// <param name="statType">변경할 스탯 타입</param>
        public virtual void ChangeStat(string statType)
        {
            // 변경할 스탯 가져오기
            EntityMoveStatSO changedStat = statInfo.MoveStats[statType];
            if (_currentMoveStat == changedStat) return; // 동일한 스탯이면 종료

            _currentMoveStat = changedStat;

            // 스탯 값 업데이트
            _moveSpeed = _currentMoveStat.MoveSpeed;
            _moveToUPSpeed = _currentMoveStat.MoveToUpSpeed;
            _upDistance = _currentMoveStat.UpDistance;
            _maxSpeed = _currentMoveStat.MaxSpeed;
            _jumpPower = _currentMoveStat.JumpPower;
            _rotateSpeed = _currentMoveStat.RotateSpeed;
            _maxSlopeAngle = _currentMoveStat.MaxSlopeAngle;
            stopPer = _currentMoveStat.StopPer;
            stopDistance = _currentMoveStat.StopDistance;
        }

        /// <summary>
        /// 점프
        /// </summary>
        public void Jump()
        {
            rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse); // 위 방향으로 힘 추가
        }

        /// <summary>
        /// 물리 업데이트 (FixedUpdate)
        /// </summary>
        protected virtual void FixedUpdate()
        {
            Move(); // 이동
            MoveMaxCheck(); // 최대 속도 확인
        }

        /// <summary>
        /// 이동 로직 (상속된 클래스에서 구현)
        /// </summary>
        protected virtual void Move()
        {
            // 상속된 클래스에서 구체적인 이동 로직 구현
        }

        /// <summary>
        /// 최대 속도 체크 및 제한
        /// </summary>
        protected void MoveMaxCheck()
        {
            Vector3 tempVelocity = rb.linearVelocity;
            tempVelocity.y = 0;
            if (tempVelocity.magnitude > _maxSpeed)
            {
                tempVelocity = tempVelocity.normalized * (_maxSpeed + 0.01f);// 최대 속도 제한
                tempVelocity.y = rb.linearVelocity.y;
                rb.linearVelocity = tempVelocity; 
            }
        }
    }
}

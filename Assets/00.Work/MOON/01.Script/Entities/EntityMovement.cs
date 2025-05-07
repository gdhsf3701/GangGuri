using _00.Work.MOON._01.Script.SO.Entity;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Entities
{
    public abstract class EntityMovement : MonoBehaviour, IEntityComponent
    {
        #region Variables

        [SerializeField] protected EntityMoveStatInfoSO statInfo; // 이동 스탯 정보

        // 현재 이동 스탯
        protected EntityMoveStatSO _currentMoveStat;
        protected EntityMoveStatPerSO _currentMoveStatPer;

        // 이동 관련 변수
        protected float _moveSpeed;
        protected float _rotateSpeed;
        protected float _maxSlopeAngle;
        protected float _stopDistance;

        // 물리 계산 및 참조
        protected Vector3 _slopeVelocity;
        protected Vector3 _velocity; // 이동 속도
        protected Entity _entity; // 엔티티 참조
        protected GroundChecker _groundChecker; // 지면 체크
        protected Transform _parent;

        #endregion

        #region Initialization

        /// <summary>
        /// 엔티티 초기화
        /// </summary>
        /// <param name="entity">초기화할 엔티티</param>
        public virtual void Initialize(Entity entity)
        {
            _entity = entity;
            ChangeStatPer("NORMAL");
            ChangeStat("NORMAL"); // 기본 스탯 설정
            _parent = entity.transform;
            _groundChecker = _entity.GetCompo<GroundChecker>(); // GroundChecker 컴포넌트 참조
        }

        #endregion

        #region Stat Management

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
            _moveSpeed = _currentMoveStat.MoveSpeed * _currentMoveStatPer.MoveSpeedPer;
            _rotateSpeed = _currentMoveStat.RotateSpeed * _currentMoveStatPer.RotateSpeedPer;
            _maxSlopeAngle = _currentMoveStat.MaxSlopeAngle;
            _stopDistance = _currentMoveStat.StopDistance;
        }
        
        public virtual void ChangeStatPer(string statType)
        {
            // 변경할 스탯 가져오기
            EntityMoveStatPerSO changedStatPer = statInfo.MoveStatsPer[statType];
            if (_currentMoveStatPer == changedStatPer) return; // 동일한 스탯이면 종료

            _currentMoveStatPer = changedStatPer;

            if (_currentMoveStat != null)
            {
                _moveSpeed = _currentMoveStat.MoveSpeed * _currentMoveStatPer.MoveSpeedPer;
                _rotateSpeed = _currentMoveStat.RotateSpeed * _currentMoveStatPer.RotateSpeedPer;
            }
        }

        #endregion

        #region Movement Logic

        /// <summary>
        /// 물리 업데이트 (FixedUpdate)
        /// </summary>
        protected virtual void FixedUpdate()
        {
            Move(); // 이동
        }

        /// <summary>
        /// 이동 로직 (상속된 클래스에서 구현)
        /// </summary>
        protected virtual void Move()
        {
            // 상속된 클래스에서 구체적인 이동 로직 구현
        }

        #endregion

        public void SetDestination(object o)
        {
            throw new System.NotImplementedException();
        }
    }
}

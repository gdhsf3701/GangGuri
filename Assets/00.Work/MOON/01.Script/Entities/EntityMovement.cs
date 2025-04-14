using _00.Work.MOON._01.Script.SO.Entity;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Entities
{
    public abstract class EntityMovement : MonoBehaviour , IEntityComponent
    {
        [SerializeField] protected EntityMoveStatInfoSO statInfo;
        [SerializeField] protected Rigidbody rb;
        
        protected EntityMoveStatSO _currentMoveStat;
        
        protected float _moveSpeed;
        protected float _slopeSpeed;
        protected float _maxAngle;
        protected float _maxSpeed;
        protected float _jumpPower;
        protected float _rotateSpeed;
        
        protected Entity _entity;

        protected GroundChecker _groundChecker;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            ChangeStat(EntityMoveStatType.Normal);
            _groundChecker = _entity.GetCompo<GroundChecker>();
        }
        
        public void ChangeStat(EntityMoveStatType statType)
        {
            EntityMoveStatSO changedStat = statInfo.MoveStats[statType];
            if (_currentMoveStat == changedStat)
                return;
            
            _currentMoveStat = changedStat;

            _moveSpeed = _currentMoveStat.MoveSpeed;
            _slopeSpeed = _currentMoveStat.SlopeSpeed;
            _maxAngle = _currentMoveStat.MaxAngle;
            _maxSpeed = _currentMoveStat.MaxSpeed;
            _jumpPower = _currentMoveStat.JumpPower;
            _rotateSpeed = _currentMoveStat.RotateSpeed;
        }
        protected void Jump()
        {
            if (_groundChecker.GroundCheck())
            {
                rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            }
        }

        protected void SlopeMove()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 2f)) {
                float angle = Vector3.Angle(Vector3.up, hit.normal);
                if (angle > _maxAngle) 
                { 
                    Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, hit.normal);
                    rb.AddForce(slopeDirection * _slopeSpeed, ForceMode.Acceleration);
                }
            }
        }

        protected virtual void FixedUpdate()
        {
            SlopeMove();
            Move();
            MoveMaxCheck();
        }

        protected virtual void Move()
        {
            
        }

        protected void MoveMaxCheck()
        {
            if (rb.linearVelocity.magnitude > _maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * (_maxSpeed + 0.01f);
            }
        }
    }
}
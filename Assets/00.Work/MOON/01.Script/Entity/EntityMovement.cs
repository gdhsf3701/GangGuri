using _00.Work.MOON._01.Script.SO.Entity;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Entity
{
    public abstract class EntityMovement : MonoBehaviour
    {
        [SerializeField] protected EntityMoveStatInfoSO statInfo;
        [SerializeField] protected Rigidbody rigidbody;
        
        protected EntityMoveStatSO _currentMoveStat;
        
        protected float _moveSpeed;
        protected float _slopeSpeed;
        protected float _maxAngle;
        protected float _maxSpeed;
        
        protected virtual void Awake()
        {
            ChangeStat(EntityMoveStatType.Normal);
        }
        
        public void ChangeStat(EntityMoveStatType statType)
        {
            EntityMoveStatSO changedStat = statInfo.MoveStats[statType];
            if (_currentMoveStat == changedStat)
            {
                print("IsEqual");
                return;
            }
            _currentMoveStat = changedStat;

            _moveSpeed = _currentMoveStat.MoveSpeed;
            _slopeSpeed = _currentMoveStat.SlopeSpeed;
            _maxAngle = _currentMoveStat.MaxAngle;
            _maxSpeed = _currentMoveStat.MaxSpeed;
        }
        
        protected void SlopeMove()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f)) {
                float angle = Vector3.Angle(Vector3.up, hit.normal);
        
                if (angle > _maxAngle) 
                { 
                    Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, hit.normal);
                    rigidbody.AddForce(slopeDirection * _slopeSpeed, ForceMode.Acceleration);
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
            if (rigidbody.linearVelocity.magnitude > _maxSpeed)
            {
                rigidbody.linearVelocity = rigidbody.linearVelocity.normalized * (_maxSpeed + 0.01f);
            }
        }
    }
}
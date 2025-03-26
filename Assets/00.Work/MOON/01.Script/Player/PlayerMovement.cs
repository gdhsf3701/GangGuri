using System;
using System.Collections.Generic;
using _00.Work.MOON._01.Script.SO;
using UnityEngine;
using UnityEngine.Serialization;

namespace _00.Work.MOON._01.Script.Player
{
    public class PlayerMovement : MonoBehaviour, IEntityComponent
    {
        [field: SerializeField]PlayerMovementTypeSO[] typeList;
        private Dictionary<MovementType, PlayerMovementTypeSO> _typeDictionary =
            new Dictionary<MovementType, PlayerMovementTypeSO>();
        private PlayerMovementTypeSO _currentType;
        public float JumpPower => _currentType.jumpPower;
        public float MoveSpeed => _currentType.moveSpeed;
        public float MaxSpeed => _currentType.maxSpeed;
        public bool IsMaxed { get; private set; }
        private Entity _entity;
        private Rigidbody _rb;
        private Vector2 _moveDir;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            foreach (PlayerMovementTypeSO type in typeList)
            {
                _typeDictionary.Add(type.Type, type);
            }
            ChangeType(MovementType.Normal);
        }

        public void ChangeType(MovementType type)
        {
            _currentType = _typeDictionary[type];
            // if (_currentType.timer > 0)
            // {
            //     
            // }
        }

        public void Initialize(Entity entity)
        {    
            _entity = entity;
        }
        public void MoveDirChanged(Vector2 change)
        {
            _moveDir = change;
        }
        public void Jump()
        {
            if (_entity.GetCompo<GroundChecker>().GroundCheck())
            {
                _rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            }
        }
        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 changedEuler = transform.eulerAngles;
            Vector3 changedVelocity = _rb.angularVelocity;
            Vector3 localforward = transform.forward;

            localforward.y = 0;
            localforward = localforward.normalized * (_moveDir.y * MoveSpeed);

            changedEuler.y += _moveDir.x;
            float checkedCanMove = Vector3.Magnitude(new Vector3(_rb.angularVelocity.x, 0, _rb.angularVelocity.z));
            if (checkedCanMove >= MaxSpeed)
            {
                IsMaxed = true;
                localforward = (MaxSpeed + 0.01f) * localforward.normalized;
                changedVelocity = -localforward; 
            }
            else
            {
                IsMaxed = false;
                changedVelocity -= localforward; 
            }
            transform.eulerAngles = changedEuler;
            _rb.angularVelocity = changedVelocity;
        }
    }
}

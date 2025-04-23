using System;
using _00.Work.MOON._01.Script.Entities;
using _00.Work.MOON._01.Script.SO.Player;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Players
{
    public class PlayerMovement : EntityMovement
    {
        [SerializeField]private Transform rotateTarget;
        public PlayerInputSO PlayerInput { get; private set; }
        private PlayerMoveStatInfoSO _statInfo;
        
        protected float _moveThresholdTime;
        
        private bool _isMoveCheck = false;
        private float _timer;
        
        public bool IsCanMove { get; private set; } = false;
        
        private float RotateTargetY => rotateTarget.eulerAngles.y;
        
        private Transform parent;
        
        private Vector3 _velocity;
        private Vector3 _movementDirection;
        
        protected void Awake()
        {
            GetMoveStatsInfo();
        }

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            parent = entity.transform;
            PlayerInput.IsMoveThreshold += IsMoveChecking;
        }
        public void OnDestroy()
        {
            PlayerInput.IsMoveThreshold -= IsMoveChecking;
        }
        
        private void IsMoveChecking(bool isMove)
        {
            _isMoveCheck = isMove;
        }

        private void GetMoveStatsInfo()
        {
            _statInfo = statInfo as PlayerMoveStatInfoSO;
            PlayerInput = _statInfo.PlayerInput;
            _moveThresholdTime = _statInfo.MoveThresholdTime;
        }
        
        public void SetMovementDirection(Vector2 movementInput , float movementProportion = 1f)
        {
            _movementDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized;
            _movementDirection *= movementProportion;
        }
        
        protected override void FixedUpdate()
        {
            if (IsCanMove)
            {
                CalculateMovement();
                Move();
            }
            else
            {
                StopImmediately();
            }
            SlopeMove();
            MoveMaxCheck();
        }
        
        private void Update()
        {
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
        
        public void StopImmediately()
        {
            _movementDirection = Vector3.zero;
        }
        private void CalculateMovement()
        {
            _velocity = Quaternion.Euler(0, RotateTargetY, 0) * _movementDirection;
            _velocity *= _moveSpeed * Time.fixedDeltaTime * 100;

            if (_velocity.magnitude > 0)
            {
                Vector3 currentEulerAngles = parent.rotation.eulerAngles;
                float targetYAngle = Quaternion.LookRotation(_velocity).eulerAngles.y;

                float changedYAngle = Mathf.LerpAngle(currentEulerAngles.y, targetYAngle, Time.fixedDeltaTime * 8f);

                parent.rotation = Quaternion.Euler(currentEulerAngles.x, changedYAngle, currentEulerAngles.z);
            }
            _velocity.y = 0;
        }

        protected override void Move()
        {
            rb.AddForce(_velocity, ForceMode.Force);
        }
    }
}

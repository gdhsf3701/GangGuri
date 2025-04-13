using _00.Work.MOON._01.Script.Entities;
using _00.Work.MOON._01.Script.SO.Player;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Players
{
    public class PlayerMovement : EntityMovement
    {
        public PlayerInputSO PlayerInput { get; private set; }
        private PlayerMoveStatInfoSO _statInfo;

        protected void Awake()
        {
            GetMoveStatsInfo();
            PlayerInput.OnJumpKeyEvent += Jump;
        }

        private void Jump()
        {
            if (_entity.GetCompo<GroundChecker>().GroundCheck())
            {
                rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            }
        }

        private void GetMoveStatsInfo()
        {
            _statInfo = statInfo as PlayerMoveStatInfoSO;
            PlayerInput = _statInfo.PlayerInput;
        }

        protected override void FixedUpdate()
        {
            Rotate();
            base.FixedUpdate();
            
        }

        private void Rotate()
        {
            
        }

        protected override void Move()
        {
            rb.AddForce(transform.forward * (PlayerInput.MovementKey.y * _moveSpeed), ForceMode.Force);
        }
    }
}

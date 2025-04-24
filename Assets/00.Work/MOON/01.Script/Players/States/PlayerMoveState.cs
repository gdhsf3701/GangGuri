using _00.Work.MOON._01.Script.Entities;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Players.States
{
    public class PlayerMoveState : PlayerState
    {
        public PlayerMoveState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnJumpKeyEvent += JumpStateChange;
        }
        public override void Exit()
        {
            base.Exit();
            _player.PlayerInput.OnJumpKeyEvent -= JumpStateChange;
        }

        public override void Update()
        {
            base.Update();
            
            _movement.SetMovementDirection(_player.PlayerInput.MovementKey);
            Vector2 movementKey = _player.Rb.linearVelocity;
            
            if(movementKey.magnitude < _inputThreshold && !_movement.IsCanMove)
            {
                _player.ChangeState("IDLE");
            }
        }
    }
}
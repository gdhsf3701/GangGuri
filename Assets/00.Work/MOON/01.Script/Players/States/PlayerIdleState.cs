using _00.Work.KLM.Sound;
using _00.Work.MOON._01.Script.Entities;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Players.States
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Entity entity, int animationHash) : base(entity, animationHash)
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
            Vector3 movementKey = _player.Rb.linearVelocity;
            
            movementKey.y = 0;
            
            if(movementKey.magnitude > _inputThreshold && _movement.IsCanMove)
            {
                _player.ChangeState("MOVE");
            }
        }
    }
}
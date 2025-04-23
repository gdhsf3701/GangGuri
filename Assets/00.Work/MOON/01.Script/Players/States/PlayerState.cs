using _00.Work.MOON._01.Script.Entities;
using _00.Work.MOON._01.Script.FSM;

namespace _00.Work.MOON._01.Script.Players.States
{
    public abstract class PlayerState : EntityState
    {
        protected Player _player;
        protected readonly float _inputThreshold = 0.1f;

        protected PlayerMovement _movement;
        
        protected GroundChecker _groundChecker;
        public PlayerState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            _movement = entity.GetCompo<PlayerMovement>();
            _groundChecker = entity.GetCompo<GroundChecker>();
        }
        protected void JumpStateChange()
        {
            if(_groundChecker.GroundCheck())
            {
                _movement.Jump();
                _player.ChangeState("JUMP");
            }
        }
    }
}
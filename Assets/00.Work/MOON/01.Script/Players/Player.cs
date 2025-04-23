using _00.Work.MOON._01.Script.Entities;
using _00.Work.MOON._01.Script.FSM;
using _00.Work.MOON._01.Script.SO.Player;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Players
{
    public class Player : Entity
    {
        [field : SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        [SerializeField] private StateDataSO[] states;
            
        [field:SerializeField] public Rigidbody Rb { get; private set; }
        
        private EntityStateMachine _stateMachine;
        
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, states);
            
        }
        private void Start()
        {
            _stateMachine.ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void ChangeState(string newStateName) => _stateMachine.ChangeState(newStateName);
    }
}

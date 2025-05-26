using System;
using _00.Work.MOON._01.Script.Core.DI;
using _00.Work.MOON._01.Script.Entities;
using _00.Work.MOON._01.Script.FSM;
using _00.Work.MOON._01.Script.SO.Player;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Players
{
    public class Player : Entity ,IDependencyProvider ,IHitable
    {
        public static Action OnFail; //실패
        
        [field : SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        [SerializeField] private StateDataSO[] states;
            
        [field:SerializeField] public Rigidbody Rb { get; private set; }
        
        private EntityStateMachine _stateMachine;
        
        [Provide]
        public Player ProvidePlayer() => this;
        
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, states);
            
        }
        
        private void Start()
        {
            _stateMachine.ChangeState("IDLE");
        }

        private void OnDisable()
        {
            _stateMachine.ChangeState("JUMP");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        private void OnDestroy()
        {
            _stateMachine.StateDestroy();
        }

        public void ChangeState(string newStateName) => _stateMachine.ChangeState(newStateName);
        public void Hit(Entity hiter)
        {
            Debug.Log("Attacked by " + hiter.gameObject.name);
            OnFail?.Invoke(); //실패함을 알려주기
        }
    }
}

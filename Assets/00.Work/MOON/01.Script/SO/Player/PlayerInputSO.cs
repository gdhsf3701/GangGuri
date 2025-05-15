using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _00.Work.MOON._01.Script.SO.Player
{
    [CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/Input/Player")]
    public class PlayerInputSO : ScriptableObject,Input.IPlayerActions
    {
        public Vector2 MovementKey {get; private set; } 
        public event Action OnJumpKeyEvent;
        
        public Action<bool> IsMoveThreshold;
        
        private Input _controls;
        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Input();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                IsMoveThreshold?.Invoke(true);
            }
            else if (context.canceled)
            {
                IsMoveThreshold?.Invoke(false);
            }
            MovementKey = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnJumpKeyEvent?.Invoke();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            
        }
    }
}

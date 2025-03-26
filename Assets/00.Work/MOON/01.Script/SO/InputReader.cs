using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _00.Work.MOON._01.Script.SO
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "SO/InputReader", order = 1)]
    public class InputReader : ScriptableObject, Input.IPlayerActions
    {
        public event Action OnDashEvent;
        public event Action OnJumpEvent;
        public event Action<Vector2> OnMoveEvent;
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
        public void OnDash(InputAction.CallbackContext context)
        {
            OnDashEvent?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            OnJumpEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }
}

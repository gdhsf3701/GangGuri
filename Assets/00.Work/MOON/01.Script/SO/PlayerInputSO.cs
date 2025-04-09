using UnityEngine;
using UnityEngine.InputSystem;

namespace _00.Work.MOON._01.Script.SO
{
    [CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/Player/Input")]
    public class PlayerInputSO : ScriptableObject,Input.IPlayerActions
    {
        public Vector2 MovementKey {get; private set; } 
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
            MovementKey = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            
        }
    }
}

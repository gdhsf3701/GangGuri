using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _00.Work.MOON._01.Script.SO.MiniGame
{
    [CreateAssetMenu(fileName = "MiniGameInput", menuName = "SO/Input/MiniGame")]
    public class MiniGameInputSO : ScriptableObject , Input.IMiniGameActions
    {
        public event Action OnMouseClick;
        private Input _controls;
        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Input();
                _controls.MiniGame.SetCallbacks(this);
            }
            _controls.MiniGame.Enable();
        }

        private void OnDisable()
        {
            _controls.MiniGame.Disable();
        }
        public void OnLeftMouse(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnMouseClick?.Invoke();
        }
    }
}

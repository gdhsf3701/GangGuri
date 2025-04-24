using _00.Work.MOON._01.Script.SO.Cam;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _00.Work.MOON._01.Script.Players
{
    public class HeadMove : MonoBehaviour
    {
        [SerializeField]private CinemachinePositionComposer cam;
        [SerializeField] private POVInputSO povInput;
        [SerializeField] private CamSettingSO camSetting;

        private float _turnSpeedX;
        private float _turnSpeedY;
        private float _goDefaultRotateTime;
        private float _resetTime;
        
        private Vector2 _xMinMax;
        private Vector2 _camDistanceMinMax;
        
        private Quaternion _camDefaultRotate;
        
        private bool _isRotateChange = false;
        private bool _isRotateStart;
        private float _timer;
        
        private bool _canRotate;
        private Vector2 _lockCursorPos = Vector2.zero;

        

        
        private void Awake()
        {
            CamSettingSOChanged();
            povInput.LookedChange += Looked;
            povInput.PointerDeltaChanged += LookChange;
            povInput.ZoomedChange += ZoomInOut;
        }

        private void ZoomInOut(float value)
        {
            cam.CameraDistance = Mathf.Clamp(cam.CameraDistance - value, _camDistanceMinMax.x, _camDistanceMinMax.y);
        }

        // private void Update()
        // {
        //     GoDefault();
        // }

        // private void GoDefault()
        // {
        //     if (_isRotateChange)
        //     {
        //         _timer -= Time.deltaTime;
        //         if (_timer <= 0)
        //         {
        //             _isRotateChange = false;
        //             _isRotateStart = true;
        //             _timer = 0;
        //         }
        //     }
        //     else if (_isRotateStart)
        //     {
        //         _timer += Time.deltaTime;
        //         transform.localRotation = Quaternion.Lerp(transform.localRotation , _camDefaultRotate, _timer * _goDefaultRotateTime);
        //         if (_timer * _goDefaultRotateTime >= 1)
        //         {
        //             _isRotateStart = false;
        //             transform.localRotation = _camDefaultRotate;
        //         }
        //     }
        // }

        private void CamSettingSOChanged()
        {
            _turnSpeedX = camSetting.TurnSpeedX;
            _turnSpeedY = camSetting.TurnSpeedY;
            _resetTime = camSetting.ResetTime;
            _goDefaultRotateTime = camSetting.GoDefaultRotateTime;
            
            _xMinMax = camSetting.XMinMax;
            _camDistanceMinMax = camSetting.CamDistanceMinMax;
            
            _camDefaultRotate = Quaternion.Euler(camSetting.CamDeffultRotate);
        }

        public void Looked(bool canRotate)
        {
            _canRotate = canRotate;
            if (canRotate)
            {
                _lockCursorPos = povInput.PointerPos;
                Cursor.lockState = CursorLockMode.Locked;
                _isRotateChange = false;
                _isRotateStart = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Mouse.current.WarpCursorPosition(_lockCursorPos);
                _timer = _resetTime;
                _isRotateChange = true;
            }
        }

        public void LookChange(Vector2 delta)
        {
            if (_canRotate == false)
                return;
            
            float rotationX = -delta.y * _turnSpeedY * Time.fixedDeltaTime;
            float rotationY = delta.x * _turnSpeedX * Time.fixedDeltaTime;

            Vector3 changedEuler = transform.eulerAngles;

            if (Mathf.Abs(changedEuler.x) > 180f) changedEuler.x -= 360f;

            changedEuler.x = Mathf.Clamp(changedEuler.x + rotationX, _xMinMax.x, _xMinMax.y);
            changedEuler.y += rotationY;

            transform.eulerAngles = changedEuler;
        }
    }
}

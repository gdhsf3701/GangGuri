using _00.Work.MOON._01.Script.SO;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Entity.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private PlayerMoveStatInfoSO statInfo;
        
        private PlayerInputSO _playerInput;
        
        private PlayerMoveStatSO[] _moveStats;
        private PlayerMoveStatSO _currentMoveStat;
        
        private float _moveSpeed;
        private float _slopeSpeed;
        private float _maxAngle;
        private float _maxSpeed;

        private void Awake()
        {
            ChangeStat("NORMAL");
        }

        private void GetMoveStatsInfo()
        {
            _playerInput = statInfo.PlayerInput;
            _moveStats = statInfo.MoveStats;
        }

        public void ChangeStat(string statName)
        {
            if(_currentMoveStat.CheckStatName(statName))
                return;
            foreach (PlayerMoveStatSO moveStat in _moveStats)
            {
                if (moveStat.CheckStatName(statName))
                {
                    _currentMoveStat = moveStat;
                    break;
                }
            }

            _moveSpeed = _currentMoveStat.MoveSpeed;
            _slopeSpeed = _currentMoveStat.SlopeSpeed;
            _maxAngle = _currentMoveStat.MaxAngle;
            _maxSpeed = _currentMoveStat.MaxSpeed;
        }
        private void SlopeMove()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f)) {
                float angle = Vector3.Angle(Vector3.up, hit.normal);
        
                if (angle > _maxAngle) 
                { 
                    Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, hit.normal);
                    rigidbody.AddForce(slopeDirection * _slopeSpeed, ForceMode.Acceleration);
                }
            }
        }
        
        private void PlayerMove()
        {
            rigidbody.AddForce(transform.forward * (_playerInput.MovementKey.y * _moveSpeed), ForceMode.Force);
        }

        
        private void FixedUpdate()
        {
            SlopeMove();
            PlayerMove();
        }
    }
}

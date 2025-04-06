using _00.Work.MOON._01.Script.SO;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Entity.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]private Rigidbody rigidbody;
        [SerializeField]private float slopeSpeed = 15f;
        [SerializeField]private float moveSpeed = 25f;
        [SerializeField] private PlayerInputSO playerInput;
        [SerializeField] private float maxAngle = 4f;
        [SerializeField] private float maxSpeed = 50f ;
        private void FixedUpdate() 
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f)) {
                float angle = Vector3.Angle(Vector3.up, hit.normal);
        
                if (angle > maxAngle) 
                { 
                    Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, hit.normal);
                    rigidbody.AddForce(slopeDirection * slopeSpeed, ForceMode.Acceleration);
                }
            }
            rigidbody.AddForce(transform.forward * (playerInput.MovementKey.y * moveSpeed), ForceMode.Force);
        }
    }
}

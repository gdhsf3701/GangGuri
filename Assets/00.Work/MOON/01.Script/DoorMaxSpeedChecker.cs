using _00.Work.MOON._01.Script.Player;
using UnityEngine;

namespace _00.Work.MOON._01.Script
{
    public class DoorMaxSpeedChecker : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out PlayerMovement movement))
            {
                if (movement.IsMaxed)
                {
                    Destroy(gameObject);
                } 
            }
        }
    }
}

using _00.Work.MOON._01.Script.Players;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Obstacle.Ice
{
    public class Ice : MonoBehaviour
    {
        private PlayerMovement move;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                move = other.gameObject.GetComponentInChildren<PlayerMovement>();

                if (!move.GetStatPerName("STOP"))
                {
                    move.ChangeStatPer("FAST");
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!move.GetStatPerName("STOP"))
                {
                    move.ChangeStatPer("NORMAL");
                }
            }
        }
    }
}
using _00.Work.MOON._01.Script.Players;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Obstacle.Trash
{
    public class Slow : MonoBehaviour
    {
        private PlayerMovement move;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                move = other.gameObject.GetComponentInChildren<PlayerMovement>();

                move.ChangeStatPer("SLOW");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                move.ChangeStatPer("NORMAL");
            }
        }
    }
}
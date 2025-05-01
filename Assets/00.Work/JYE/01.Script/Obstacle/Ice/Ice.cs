using _00.Work.MOON._01.Script.Players;
using System.Collections;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Obstacle.Ice
{
    public class Ice : MonoBehaviour
    {
        private PlayerMovement move;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                move = collision.gameObject.GetComponentInChildren<PlayerMovement>();

                move.ChangeStatPer("FAST");
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                move.ChangeStatPer("NORMAL");
            }
        }
    }
}
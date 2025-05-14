using System;
using _00.Work.MOON._01.Script.Players;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Success
{ 
    //모든 스테이지에 필요
    public class EnterPinch : MonoBehaviour
    {
        [Header("Need")]
        [SerializeField] private GameObject pinchUI; //훔치는 UI
        private PlayerMovement move;

        private void Awake()
        {
            pinchUI.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                move = other.gameObject.GetComponentInChildren<PlayerMovement>();

                move.ChangeStatPer("STOP");
            }
        }
    }
}

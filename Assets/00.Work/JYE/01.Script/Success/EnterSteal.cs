using System;
using _00.Work.JYE._01.Script.Manager;
using _00.Work.MOON._01.Script.Players;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Success
{ 
    //모든 스테이지에 필요
    public class EnterSteal : MonoBehaviour
    {
        [Header("Need")]
        [SerializeField] private SceneChange sceneManager; //씬 바꾸기 위해

        private void Awake()
        {
            Player.OnFail += Fail;
        }

        [ContextMenu("testF")]
        public void Fail() //실패함
        {
            sceneManager.FailSceneBtn();
        }

        private void OnTriggerEnter(Collider other) //목적지 도착.
        {
            if (other.gameObject.CompareTag("Player"))
            {
                sceneManager.StealSceneBtn();
            }
        }

        [ContextMenu("testS")]
        public void test()
        {
            sceneManager.StealSceneBtn();
        }

        private void OnDisable()
        {
            Player.OnFail -= Fail;
        }
    }
}

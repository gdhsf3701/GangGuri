using System;
using _00.Work.JYE._01.Script.Save;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Success
{
    //성공씬만
    public class SuccessScene : MonoBehaviour
    {
        [Header("Need")]
        [SerializeField] private int curStage; //현재 스테이지

        private void Awake()
        {
            AddCanStage();
        }

        private void Update()
        {
            AddCanStage();
        }

        private void AddCanStage() //가능한 스테이지 늘리기
        {
            if (SaveManager.CurrentData.stage < curStage + 1)
            {
                SaveManager.CurrentData.stage = curStage + 1;
            }
        }
    }
}

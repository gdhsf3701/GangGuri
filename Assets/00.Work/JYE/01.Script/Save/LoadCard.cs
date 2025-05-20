using System;
using System.IO;
using TMPro;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    //로드씬 로드 카드에만 필요
    public class LoadCard : MonoBehaviour
    {
        public static Action<int> OnCardDelete; //카드 삭제 할 때
        
        [Header("Show")] [SerializeField] private int myNum; //자신의 번호 (저장 슬롯)

        [Space(10)] [Header("Need")]
        [SerializeField] private TextMeshProUGUI carNum; //가진 차 개수
        [SerializeField] private TextMeshProUGUI stageNum; //클리어한 스테이지 수
        [SerializeField] private TextMeshProUGUI playTime; //마지막 플레이 시간

        private string path; //저장 값
        private GameSaveData saveData; //(이 카드)저장 값

        private void Awake()
        {
            path =  SaveManager.Path;
            
            myNum = transform.parent.childCount; //자신의 번째

            SetCard();
        }

        public void Load() //불러오기 (자기 자신을 누름)
        {
            SaveManager.Instance.SetSaveData(saveData, myNum);
        }
        
        public void DeleteBtn() //삭제 버튼
        {
            OnCardDelete?.Invoke(myNum);
            Destroy(gameObject);
        }

        private void SetCard() //카드 세팅하기
        {
            string data = File.ReadAllText($"{path}/{myNum}"); //찾기
            saveData = JsonUtility.FromJson<GameSaveData>(data);

            int car = 0;
            foreach (var item in saveData.playerCar) //true 값(즉 소지 값) 세기
            {
                if (item)
                {
                    car++;
                }
            }

            //텍스트 입력
            carNum.text =$"가진 차 개수 : {car}";
            stageNum.text =$"플레이 가능한 스테이지 : {saveData.stage}";
            playTime.text =$"마지막 플레이 시간 : {saveData.finalDate}";  //현재 시간임 (나중 수정)
        }


    }
}

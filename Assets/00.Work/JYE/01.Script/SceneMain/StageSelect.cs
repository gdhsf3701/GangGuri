using System.Collections;
using _00.Work.JYE._01.Script.Manager;
using _00.Work.JYE._01.Script.Save;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.JYE._01.Script.SceneMain
{
    //스테이지 선택씬만 필요
    public class StageSelect : MonoBehaviour
    {
        [Header("Show")]
        [SerializeField] private int stageCount; //현 스테이지
        [Space(10)]
        [Header("Setting")]
        [SerializeField] private int minStage = 1; //마지막 스테이지
        [SerializeField] private int maxStage = 5; //첫 스테이지
        [Space(10)]
        [Header("Need")]
        [SerializeField] private SceneChange sceneManager; //씬을 바꾸기 위해
        [SerializeField] private GameObject rockImage; //잠금 이미지
        [SerializeField] private GameObject warning; //잠금 이미지
        [SerializeField] private Sprite[] stageImages; //스테이지 버튼에 보여줄 이미지들
    
        private Image currentStageImage; //씬 이미지

        private GameSaveData curData; //현재 저장된 데이터
        private bool canStage; // true : 잠금이 아님 / false : 잠금
        
        private void Awake()
        {
            curData = SaveManager.CurrentData;
            
            rockImage.SetActive(false);
            warning.SetActive(false);
            currentStageImage = GetComponent<Image>();
            stageCount = minStage - 1;
            NextBtn();
        }

        public void StageBtn() //스테이지 버튼
        {
            if (canStage)
            {
                sceneManager.StageSceneBtn(stageCount); //씬 이동
            }
            else
            {
                StartCoroutine(Warning());
            }
        }

        public void NextBtn() //다음 버튼
        {
            stageCount++;
            if (stageCount > maxStage)
            {
                stageCount = 1;
            }
            
            SetStageImage();
        }

        public void BeforeBtn() //이전 버튼
        {
            stageCount--;
            if (stageCount < minStage)
            {
                stageCount = maxStage;
            }

            SetStageImage();
        }

        private void SetStageImage() //락이미지나 스테이지 이미지등 세팅
        {
            currentStageImage.sprite = stageImages[stageCount-1];

            canStage = (stageCount - minStage) < curData.stage; //플레이 가능 스테이지보다 적다면
            
            rockImage.SetActive(!canStage); //풀린 스테이지인지
            print($"{!canStage} cu : {stageCount}/ all {curData.stage}");

        }

        private IEnumerator Warning() //경고문 띄우기
        {
            warning.SetActive(true);
            yield return new WaitForSeconds(1.2f);
            warning.SetActive(false);
        }
    }
}

using System;
using _00.Work.JYE._01.Script.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.JYE._01.Script.StealUI
{
    public class ClockUI : MonoBehaviour
    {
        public static Action OnSuccess; //성공
        public static Action OnFail; //실패
        
        [Header("Setting")]
        [SerializeField] private float timeLimit = 20; //타이머
        [SerializeField] private MonoBehaviour[] isStopScript; //작동 멈출 스크립트
        [Header("Show")]
        [SerializeField] private float coolTime; //남은 시간
        [Header("Need")]
        [SerializeField] private Image timeImage; //검은 이미지
        [SerializeField] private SceneChange sceneManager; //씬 바꾸기 위해
        [SerializeField] private GameObject Explanation; //예

        private bool isChange; //true : 씬을 바꿈 / false : 씬을 안 바꿈
        private bool isStart; // true : 시작함 / false : 시작 전

        private void Awake()
        {
            Time.timeScale = 0;
            isStart = false;
            Explanation.SetActive(true);
            coolTime = timeLimit;

            OnSuccess += Success;
            OnFail += Fail;
        }

        public void StartBtn() //시작 버튼 누름
        {
            Explanation.SetActive(false);
            Time.timeScale = 1;
            isStart = true;
        }

        private void Success() //성공함
        {
            sceneManager.SuccessSceneBtn();
            isChange = true;
        }
        private void Fail() //실패
        {
            sceneManager.FailSceneBtn();
            isChange = true;
        }

        private void Update()
        {
            if (isStart)
            {
                if (coolTime > 0)
                {
                    coolTime -= Time.deltaTime;
                    timeImage.fillAmount = 1- (coolTime/timeLimit);
                }
                else if (!isChange) //시간초과 해서 실패 씬으로
                {
                    Fail();
                }
            }

            if (isStopScript != null)
            {
                foreach (var sc in isStopScript)
                {
                    sc.gameObject.SetActive(isStart);
                }
            }
        
        }
    }
}

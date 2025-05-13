using System;
using _00.Work.JYE._01.Script.Manager;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    [Header("Show")]
    [SerializeField] private int stageCount; //현 스테이지
    [Space(10)]
    [Header("Setting")]
    [SerializeField] private int maxStage; //마지막 스테이지
    [Space(10)]
    [Header("Need")]
    [SerializeField] private SceneChange sceneManager; //씬을 바꾸기 위해
    [Tooltip("1스테이지 -1 값")]
    [SerializeField] private int minValue; // 최소(-1) 값
    [SerializeField] private Sprite[] stageImages; //스테이지 버튼에 보여줄 이미지들
    
    private Image currentStageImage; //이미지

    private void Awake()
    {
        currentStageImage = GetComponent<Image>();
        stageCount = minValue;
        NextBtn();
    }

    public void StageBtn() //스테이지 버튼
    {
        sceneManager.StageSceneBtn(minValue+stageCount);
    }

    public void NextBtn() //다음 버튼
    {
        stageCount++;
        if (stageCount > maxStage)
        {
            stageCount = 1;
        }
        currentStageImage.sprite = stageImages[stageCount-1];
    }

    public void BeforeBtn() //이전 버튼
    {
        stageCount--;
        if (stageCount <= minValue)
        {
            stageCount = maxStage;
        }
        currentStageImage.sprite = stageImages[stageCount-1];
    }
}

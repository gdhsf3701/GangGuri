using System.Collections;
using _00.Work.JYE._01.Script.Save;
using _00.Work.MOON._01.Script.SO.Player;
using UnityEngine;

namespace _00.Work.JYE._01.Script.MainScene
{
    public class SelectCar : MonoBehaviour
    {
        [Header("Show")]
        [SerializeField] private GameObject currentCar; //현재 차
        [SerializeField] private int currentNum; //현재 (차 번째)번호 
        [Header("Need")]
        [SerializeField] private PlayerMoveStatSO[] allCar; //모든 차들 (종류)
        [SerializeField] private GameObject rockImage; //잠금 이미지 (나중에 걍 흑백으로 바뀔수도)
        [SerializeField] private GameObject warning; //경고 (이 차는 얻지 못함)
        [SerializeField] private GameObject carP; //차의 부모가 될 (위치)

        [SerializeField]private GameSaveData curData; //현재 저장된 데이터
        
        private int min; // 최소 번째
        private int max; // 최소 번째
        private bool isRock; // true : 못 얻음 / false : 얻음
        
        private void Awake()
        {
            curData = SaveManager.CurrentData;
            
            rockImage.SetActive(false);
            warning.SetActive(false);
            min = 0;
            max = allCar.Length - 1;
            
            currentNum = min - 1;
            NextBtn();
        }

        public void SelectBtn() //선택 버튼
        {
            if (isRock)
            {
                StartCoroutine(Warning());
            }
            else
            {
                print("select");
            }
        }

        public void NextBtn() //다음 버튼
        {
            currentNum++;
            if (currentNum > max)
            {
                currentNum = min;
            }
            
            SetCar();
        }

        public void BeforeBtn() //이전 버튼
        {
            currentNum--;
            if (currentNum < min)
            {
                currentNum = max;
            }

            SetCar();
        }

        private void SetCar() //락이미지나 스테이지 이미지등 세팅
        {
            foreach (Transform i in carP.transform) //비워주고
            {
                Destroy(i.gameObject);
            }
            GameObject c = Instantiate(allCar[currentNum].CarPrefab, carP.transform); //생성하기
            c.transform.SetParent(carP.transform);
            c.transform.localPosition = Vector3.zero;
            c.SetActive(true);
            
            
            isRock = !curData.playerCar[currentNum]; //얻은 차인지 판별
            
            rockImage.SetActive(isRock); //풀린 스테이지인지

        }

        private IEnumerator Warning() //경고문 띄우기
        {
            warning.SetActive(true);
            yield return new WaitForSeconds(1.2f);
            warning.SetActive(false);
        }
        
    }
}

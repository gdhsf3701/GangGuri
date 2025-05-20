using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.JYE._01.Script.StealUI.Stage2
{
    public class ClcikHandUI : MonoBehaviour
    {
        [Header("Setting")]
        [SerializeField] private Sprite[] eyeImages; //눈 이미지들 (0,1,2)
        [SerializeField] private float move; //움직이는 정도
        [SerializeField] private float final; //목적지 까지의 값
        [Header("Need")]
        [SerializeField] private Image eye; //눈
        [SerializeField] private GameObject right; //오른 손
        [SerializeField] private GameObject left; //왼 손
        [SerializeField]private ClcokUI clock; //성공 유무

        private bool isEye; //true : 눈을 뜸/ false : 눈을 감음
        private int countClcik; //손을 누른 수를 셈 (0,1,2,3)
        private int maxCount = 5; //이 횟수 이상 누르면 눈 뜨고 숫자 초기화
        private float currentMove; //현재 움직여진 정도
        private float curTime; //시간 재기

        private void Awake()
        {
            currentMove = 0;
            StartCoroutine(OpenEye());
        }

        private void Update()
        { 
            curTime += Time.deltaTime;
            
            if (curTime >= 2) //3초 정도 지남
            {
                if (currentMove > 0) //완전히 닫힌게 아니면
                {
                    MoveHand(-move); //점점 닫히기
                }
                curTime = 0;
            }
        }

        public void GetJewel() //보석을 누름
        {
            if (currentMove >= final)
            {
                clock.Success();
            }
        }


        public void ClcikHand() //손을 누름
        {
            if (isEye) //눈 떴는데 누름
            {
                clock.Fail();
            }
            
            
            
            countClcik++;
            if (countClcik >= maxCount - 1)
            {
                eye.sprite = eyeImages[1]; //경고
            }
            if (countClcik >= maxCount) //눈 뜨기
            {
                StartCoroutine(OpenEye());
                return;
            }
            MoveHand(move);
        }

        private void MoveHand(float value) //손 움직임
        {
            right.transform.position += new Vector3(value, 0, 0);
            left.transform.position -= new Vector3(value, 0, 0);
            
            currentMove += value;
        }

        private IEnumerator OpenEye() //눈을 뜸
        {
            isEye = true;
            eye.sprite = eyeImages[2];
            yield return new WaitForSeconds(1.5f); //0.5동안 눈을 뜸
            MoveHand(-move);
            eye.sprite = eyeImages[0];
            isEye = false;
            
            countClcik = 0; //수 초기화
        }

    }
}

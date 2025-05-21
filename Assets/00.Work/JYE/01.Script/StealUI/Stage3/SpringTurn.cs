using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _00.Work.JYE._01.Script.StealUI.Stage3
{
    public class SpringTurn : MonoBehaviour
    {
        [Header("Setting")]
        [SerializeField] private int max = 9;
        [SerializeField] private int min = 7;
        [SerializeField] private float[] checkPoint = new float[]{270, 180, 90, 10}; //체크 하는 부분들
        [FormerlySerializedAs("spring")]
        [Header("Show")]
        [SerializeField]private float turnCount; // 돌린 횟수
        [SerializeField]private int doTurnCount; //돌려야하는 횟수
        [Header("Need")]
        [SerializeField] private RectTransform springPos; //태엽 위치
        [SerializeField] private GameObject spring; //태엽
        [SerializeField] private Slider glass; //유리 (슬라이더)
        [SerializeField] private GameObject effect;//반짝임
        
        private float angle; // 지금의 회전 각도
        
        private bool isTurn; // true : 돌자(태엽 클릭) / false : 안 돌고
        private int step; //돌림 단계 0 : 안 돌림/ 1 ~ 3 : (지났는지) 확인/ 4 : 완료

        private void Awake()
        {
            effect.SetActive(false);
            SetTurn();
        }

        private void Update()
        {
            glass.value = turnCount/doTurnCount; //유리 위치 정하기
            Vector3 mosPos = Mouse.current.position.value; //마우스 위치
            
            Vector2 wantPos; //0,0을 태엽 위치로 조정한 보정 위치
            RectTransformUtility.ScreenPointToLocalPointInRectangle(springPos, mosPos, null, out wantPos);
            
            angle = ( -Mathf.Atan2(wantPos.x, wantPos.y) * Mathf.Rad2Deg + 360f ) % 360f; //알맞은 각도
            
            if (isTurn)
            {
                spring.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                step = (step,angle) switch //각도(+지난 단계) 에 따른 단계
                {
                    (0, var a)when a >= checkPoint[step+1] && a < checkPoint[step]  => 1,
                    (1, var a)when a >= checkPoint[step+1] && a < checkPoint[step]  => 2,
                    (2, var a)when a >= checkPoint[step+1] && a < checkPoint[step]  => 3,
                    (3, var a)when a <= checkPoint[step]  => 4,
                    _ => step
                };

                if (step >= 4) //다 돌았음
                {
                    turnCount++;
                    step = 0;
                }

            }
            else
            {
                spring.transform.rotation = Quaternion.Euler(new Vector3(0, 0 , 0));
                step = 0;
            }

            if (turnCount >= doTurnCount)
            {
                effect.SetActive(true);
            }
        }

        public void ClickSpring() //태엽 선택
        {
            isTurn = true;
        }

        public void CancelSpring() //손 놓음
        {
            isTurn = false;
        }

        public void ClcikJewel() //보석 클릭
        {

            if (turnCount >= doTurnCount)
            {
                ClockUI.OnSuccess();
            }
        }

        private void SetTurn() //돌려야 하는 횟수 정하기
        {
            doTurnCount = Random.Range(min, max+1);
        }
    }
}

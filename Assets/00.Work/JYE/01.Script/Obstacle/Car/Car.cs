using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Obstacle.Car
{
    public class Car : MonoBehaviour
    {
        [Header("Setting")]
        private float speed = 55; //움직이는 속도
        [SerializeField] private float stopTime; //멈춰 있는 정도
        [Space(10f)]
        [Header("Need")]
        [SerializeField] private Transform pos1; //좌표 1
        [SerializeField] private Transform pos2; //좌표 2

        private Vector3 target; //타겟 위치 (위 좌표들)
        private bool isPos; // true : 다음 타겟 pos2 / false : 다음 타겟 pos1
        private bool canMove; // true : 움직임 가능 / false : 움직임 불가
        
        private void Awake()
        {
            gameObject.transform.position = pos1.position; //첫 시작 위치
            target = pos2.position;
            isPos = true;
            canMove = true;
        }

        private void Update()
        {    float distance = Vector3.Distance(transform.position, target);
            if (distance <= 1.0f && canMove)
            {
                print("ok");
                StartCoroutine(CarMove()); // 타겟 변경
            }
            else if (canMove)
            {
                CarLook();
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, Time.deltaTime * speed); //이동
            }
        }

        private void CarLook() // 바라보는 방향
        {
            Vector3 direction = (target - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);
            }
        }

        
        private IEnumerator CarMove() //차 이동하는 거
        {
            canMove = false;
            yield return new WaitForSeconds(stopTime);
            target = isPos ? pos1.position : pos2.position; //어딜 타겟으로 할지 정하기
            isPos = !isPos;
            canMove = true;
        }
    }
}

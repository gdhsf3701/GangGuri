using System;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Obstacle.CCTV
{
    public class CCTV : MonoBehaviour
    {
        public static Action OnCCTV; //cctv�� ���� ��
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnCCTV?.Invoke();
                print("�÷��̾�� ���");
            }
        }
    }
}

using System;
using Unity.VisualScripting;
using UnityEngine;


namespace _00.Work.JYE._01.Script.Obstacle.Fog
{
    public class Fog : MonoBehaviour
    {
        [Header("Setting")]
        [SerializeField]private float speed;
        [SerializeField]private float maxValue = 1000f; //최대 안개 값 (안개가 거의 안 보임)
        [SerializeField]private float minValue = 30f; //최소 안개 값 (안개가 잘 보임)
        [Space(10f)]
        [Header("Show")]
        [SerializeField] private float endValue; //현 안개 흐림 값
        [SerializeField] private bool onFog; //true : 안개 / false : 안개 없음


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                onFog = true;
                RenderSettings.fog = true; 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                onFog = false;
            }
        }

        private void Update()
        {
            RenderSettings.fogEndDistance  = endValue;
            endValue = onFog switch
            {
                //안개 활성화 일 때
                true => Mathf.MoveTowards(endValue, minValue, Time.deltaTime* speed),
                
                //비활성화고 수가 크다면
                false => Mathf.MoveTowards(endValue, maxValue, Time.deltaTime* speed)
            };

            if (endValue > maxValue - 0.5f)
            {
                RenderSettings.fog = onFog;  // 혹시 방금 활성화 되었을 수도 있으니까.
            }
        }
    }
}

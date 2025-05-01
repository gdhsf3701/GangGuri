using System.Collections;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Obstacle.Obstacle
{
    public class Hailstone : MonoBehaviour
    {
        [Header("Setting")]
        [SerializeField] private float minTime = 0.2f;
        [SerializeField] private float maxTime = 1.2f;

        [SerializeField] private float minSize = 2f;
        [SerializeField] private float maxSize = 7f;

        [Header("Need")]
        [SerializeField] private HailstoneManager manager; //위치
        [SerializeField] private GameObject hailstons; //우박

        private Rigidbody rb;
        private bool isCoroutines; //코루틴 실행중인지.

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            HailstoneManager.OnStopHailstone += Stop;
            HailstoneManager.OnStartHailstone += Drop;

            StopAllCoroutines();
            hailstons.SetActive(false);
        }

        private void OnDisable()
        {
            HailstoneManager.OnStopHailstone -= Stop;
            HailstoneManager.OnStartHailstone -= Drop;
            StopAllCoroutines();
        }

        private void Stop() //플레이어 나감
        {
            StopAllCoroutines();
            StartCoroutine(StopHailston());
            StopAllCoroutines();
            hailstons.SetActive(false);
            isCoroutines = false;
        }

        private void Drop() //플레이어 들어옴
        {
            if(!isCoroutines)
            {
                isCoroutines=true;
                StartCoroutine(DropHailston());
            }
        }

        public void SetManager(HailstoneManager ma) //매니저 정해주기
        {
            manager = ma;
        }
        private IEnumerator StopHailston() //우박 멈추기 (자연 스럽게)
        {
            rb.isKinematic = true;
            yield return new WaitForSeconds(Random.Range(minTime, maxTime)); //기다리기
        }
        private IEnumerator DropHailston() //우박 떨어지기
        {
            rb.isKinematic = false;
            while (true)
            {
                float stonSize = Random.Range(minSize, maxSize);

                SetWorldScale(hailstons.transform, Vector3.one * stonSize); //사이즈 정하기
                SetPosition();

                yield return new WaitForSeconds(Random.Range(minTime, maxTime)); //기다리기

                hailstons.SetActive(true);

                yield return new WaitForSeconds(2); //기다리기
                hailstons.SetActive(false);
            }
        }

        private void SetPosition() // 위치 정하기
        {
            Vector3 center = manager.transform.position;
            Vector3 scale = manager.transform.localScale * 0.5f;

            float randomX = Random.Range(-scale.x, scale.x);
            float randomZ = Random.Range(-scale.z, scale.z);

            // 로컬 기준 랜덤 위치
            Vector3 localRandomPos = new Vector3(randomX, (scale.y * 2) - (randomX / 2), randomZ);

            // 로컬 -> 월드 좌표 변환 (회전 고려)
            Vector3 spawnPosition = localRandomPos;


            gameObject.transform.position = spawnPosition;
        }


        private void SetWorldScale(Transform t, Vector3 worldScale) //크기 변환
        {
            Vector3 parentScale = gameObject.transform.parent != null ? gameObject.transform.parent.lossyScale : Vector3.one;
            t.localScale = new Vector3(
                worldScale.x / parentScale.x,
                worldScale.y / parentScale.y,
                worldScale.z / parentScale.z
            );
        }
    }
}

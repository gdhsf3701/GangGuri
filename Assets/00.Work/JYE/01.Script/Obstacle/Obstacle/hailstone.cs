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
        [SerializeField] private HailstoneManager manager; //��ġ
        [SerializeField] private GameObject hailstons; //���

        private Rigidbody rb;
        private bool isCoroutines; //�ڷ�ƾ ����������.

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

        private void Stop() //�÷��̾� ����
        {
            StopAllCoroutines();
            StartCoroutine(StopHailston());
            StopAllCoroutines();
            hailstons.SetActive(false);
            isCoroutines = false;
        }

        private void Drop() //�÷��̾� ����
        {
            if(!isCoroutines)
            {
                isCoroutines=true;
                StartCoroutine(DropHailston());
            }
        }

        public void SetManager(HailstoneManager ma) //�Ŵ��� �����ֱ�
        {
            manager = ma;
        }
        private IEnumerator StopHailston() //��� ���߱� (�ڿ� ������)
        {
            rb.isKinematic = true;
            yield return new WaitForSeconds(Random.Range(minTime, maxTime)); //��ٸ���
        }
        private IEnumerator DropHailston() //��� ��������
        {
            rb.isKinematic = false;
            while (true)
            {
                float stonSize = Random.Range(minSize, maxSize);

                SetWorldScale(hailstons.transform, Vector3.one * stonSize); //������ ���ϱ�
                SetPosition();

                yield return new WaitForSeconds(Random.Range(minTime, maxTime)); //��ٸ���

                hailstons.SetActive(true);

                yield return new WaitForSeconds(2); //��ٸ���
                hailstons.SetActive(false);
            }
        }

        private void SetPosition() // ��ġ ���ϱ�
        {
            Vector3 center = manager.transform.position;
            Vector3 scale = manager.transform.localScale * 0.5f;

            float randomX = Random.Range(-scale.x, scale.x);
            float randomZ = Random.Range(-scale.z, scale.z);

            // ���� ���� ���� ��ġ
            Vector3 localRandomPos = new Vector3(randomX, (scale.y * 2) - (randomX / 2), randomZ);

            // ���� -> ���� ��ǥ ��ȯ (ȸ�� ����)
            Vector3 spawnPosition = localRandomPos;


            gameObject.transform.position = spawnPosition;
        }


        private void SetWorldScale(Transform t, Vector3 worldScale) //ũ�� ��ȯ
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

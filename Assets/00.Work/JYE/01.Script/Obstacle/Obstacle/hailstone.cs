using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Hailstone : MonoBehaviour
{
    [SerializeField] private GameObject manager; //��ġ
    [SerializeField]private GameObject hailstons; //���

    private float minTime = 0.2f;
    private float maxTime = 1.2f;

    private float minSize = 0.8f;
    private float maxSize = 2f;


    private void OnEnable()
    {
        StartCoroutine(DropHailston());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    //���� ��ġ �����ֱ�
    private IEnumerator DropHailston() //��� ��������
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime)); //��ٸ���

            float stonSize = Random.Range(minSize, maxSize);

            SetWorldScale(hailstons.transform, Vector3.one * stonSize); //������ ���ϱ�

            hailstons.SetActive(true);

            yield return new WaitForSeconds(2); //��ٸ���
            hailstons.SetActive(false);
        }
    }

    private void SetPosition() //��ġ ���ϱ�
    {

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

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Hailstone : MonoBehaviour
{
    [SerializeField] private GameObject manager; //위치
    [SerializeField]private GameObject hailstons; //우박

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

    //랜덤 위치 정해주기
    private IEnumerator DropHailston() //우박 떨어지기
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime)); //기다리기

            float stonSize = Random.Range(minSize, maxSize);

            SetWorldScale(hailstons.transform, Vector3.one * stonSize); //사이즈 정하기

            hailstons.SetActive(true);

            yield return new WaitForSeconds(2); //기다리기
            hailstons.SetActive(false);
        }
    }

    private void SetPosition() //위치 정하기
    {

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

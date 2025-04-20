using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hailstone : MonoBehaviour
{
    [SerializeField] private ObstacleSO so; //구분
    [SerializeField] private GameObject hailstonPrefabs; //우박 프리팹
    private List<GameObject> hailstons = new List<GameObject>();
    private float minTime = 1;
    private float maxTime = 2.5f;

    private float minSize = 0.8f;
    private float maxSize = 2f;


    private void Awake()
    {
        SetHailstonCount(7);
    }

    private void SetHailstonCount(int count) //일단 우박들 불러와 주기
    {
        hailstons.Clear();
        for(int i = 0; i < count; i++)
        {
            GameObject prefabs = Instantiate(hailstonPrefabs, transform);
            prefabs.SetActive(false);
            hailstons.Add(prefabs);
        }
    }

    //랜덤 위치 정해주기
    private IEnumerator DropHailston() //우박 떨어지기
    {
        for(int i = 0; i < hailstons.Count; i++)
        {
            if (!hailstons[i].activeSelf)
            {
                yield return new WaitForSeconds(Random.Range(minTime, maxTime)); //기다리기
                float stonSize = Random.Range(minSize, maxSize);

                SetWorldScale(hailstons[i].transform, Vector3.one * stonSize);

                hailstons[i].SetActive(true);
            }
        }
    }

    void SetWorldScale(Transform t, Vector3 worldScale) //크기 변환
    {
        Vector3 parentScale = t.parent != null ? t.parent.lossyScale : Vector3.one;
        t.localScale = new Vector3(
            worldScale.x / parentScale.x,
            worldScale.y / parentScale.y,
            worldScale.z / parentScale.z
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DropHailston());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
        if (other.gameObject.CompareTag("GameController"))
        {
            other.gameObject.SetActive(false);
        }
    }
}

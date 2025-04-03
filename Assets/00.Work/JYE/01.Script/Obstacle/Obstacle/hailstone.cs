using NUnit.Framework;
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

    private void DropHailston() //우박 떨어지기
    {
        for(int i = 0; i < hailstons.Count; i++)
        {
            hailstons[i].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DropHailston();
        }
    }
}

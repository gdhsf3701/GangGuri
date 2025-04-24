using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailstoneManager : MonoBehaviour
{
    [SerializeField] private ObstacleSO so; //구분
    [SerializeField] private GameObject hailstonPrefabs; //우박 프리팹
    private List<GameObject> hailstons = new List<GameObject>();


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

    private void ActiveHailston()
    {
        for(int i = 0;i < hailstons.Count;i++)
        {
            hailstons[i].SetActive(true);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ActiveHailston();
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

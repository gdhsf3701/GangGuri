using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailstoneManager : MonoBehaviour
{
    public static Action OnStartHailstone;
    public static Action OnStopHailstone;

    [SerializeField] private ObstacleSO so; //����
    [SerializeField] private GameObject hailstonPrefabs; //��� ������
    [SerializeField]private List<GameObject> hailstons = new List<GameObject>();

    private int maxCount; // ���� ū ��


    private void Awake()
    {
        SetCount();
        SetHailstonCount(maxCount);
    }

    private void SetHailstonCount(int count) //�ϴ� ��ڵ� �ҷ��� �ֱ�
    {
        hailstons.Clear();
        for(int i = 0; i < count; i++)
        {
            GameObject prefabs = Instantiate(hailstonPrefabs, transform);
            prefabs.GetComponent<Hailstone>().SetManager(this);
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

    private void SetCount()
    {
        Vector3 scale = transform.localScale;

        maxCount = scale.x > scale.z? (int)scale.x/3 : (int)scale.z/3;  //x�� z�� ��
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ActiveHailston();
            OnStartHailstone?.Invoke();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            OnStopHailstone?.Invoke();
        }
        //if (other.gameObject.CompareTag("GameController"))
        //{
        //    other.gameObject.SetActive(false);
        //}
    }
}

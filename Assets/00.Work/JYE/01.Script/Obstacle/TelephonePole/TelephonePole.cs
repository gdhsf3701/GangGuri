using UnityEngine;
using _00.Work.MOON._01.Script.SO.Player;
using System.Collections;
using System;

public class TelephonePole : MonoBehaviour
{
    [Header("setting")]
    [SerializeField]private float time; //�� ������ �ð�
    [Space(10f)]
    [Header("Need")]
    [SerializeField] private PlayerInputSO inputSO; //��ǲ so

    private bool cantMove; //true : �� ������, false : Ǯ��

    private void Update()
    {
        if(cantMove)
        {
            inputSO.IsMoveThreshold?.Invoke(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!cantMove&&collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rd = collision.gameObject.GetComponent<Rigidbody>();
            rd.isKinematic = true;
            StartCoroutine(CantMove());
            rd.isKinematic = false;
        }
    }

    private IEnumerator CantMove() //�ð� ��ŭ �� ������.
    {
        cantMove = true;
        yield return new WaitForSeconds(time);
        cantMove = false;
    }
}

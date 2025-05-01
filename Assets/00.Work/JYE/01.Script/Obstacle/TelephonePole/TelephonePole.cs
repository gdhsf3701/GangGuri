using UnityEngine;
using _00.Work.MOON._01.Script.SO.Player;
using System.Collections;
using System;

public class TelephonePole : MonoBehaviour
{
    [Header("setting")]
    [SerializeField]private float time; //못 움직일 시간
    [Space(10f)]
    [Header("Need")]
    [SerializeField] private PlayerInputSO inputSO; //인풋 so

    private bool cantMove; //true : 못 움직임, false : 풀림

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

    private IEnumerator CantMove() //시간 만큼 못 움직임.
    {
        cantMove = true;
        yield return new WaitForSeconds(time);
        cantMove = false;
    }
}

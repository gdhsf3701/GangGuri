using System;
using UnityEngine;

public class CamaraRotateChage : MonoBehaviour
{
    [SerializeField]private Transform _playerTrans;

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        Vector3 changedEuler = transform.eulerAngles;
        changedEuler.y = _playerTrans.eulerAngles.y + 90;
        transform.eulerAngles = changedEuler;
    }
}

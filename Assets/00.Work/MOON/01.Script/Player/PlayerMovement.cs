using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IEntityComponent
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpPower = 5f;
    private Entity _entity;
    private Rigidbody _rb;
    private Vector2 moveDir;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }
    public void MoveDirXChanged(Vector2 change)
    {
        moveDir = change;
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 changedEuler = transform.eulerAngles;
        Vector3 changedVelocity = _rb.angularVelocity;
        Vector3 localforward = transform.forward;

        localforward.y = 0;
        localforward = localforward.normalized;

        changedEuler.y += moveDir.x;
        changedVelocity -= localforward * moveDir.y;

        transform.eulerAngles = changedEuler;
        _rb.angularVelocity = changedVelocity;
    }
}

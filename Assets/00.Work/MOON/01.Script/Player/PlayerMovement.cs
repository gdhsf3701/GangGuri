using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IEntityComponent
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpPower = 5f;
    private Entity _entity;
    private Rigidbody _rb;
    private float moveDirX = 0;
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
        moveDirX = change.x;
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 changedEuler = transform.eulerAngles;
        changedEuler.y += moveDirX;
        transform.eulerAngles = changedEuler;
    }
}

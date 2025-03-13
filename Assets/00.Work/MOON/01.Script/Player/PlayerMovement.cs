using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IEntityComponent
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpPower = 5f;
    private Entity _entity;
    private float moveDirX = 0;

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
    }
}

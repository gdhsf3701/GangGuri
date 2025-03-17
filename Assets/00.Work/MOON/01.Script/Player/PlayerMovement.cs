using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IEntityComponent
{
    [SerializeField] float moveSpeed = 5f;
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
    public void MoveDirChanged(Vector2 change)
    {
        moveDir = change;
    }
    public void Jump()
    {
        if (_entity.GetCompo<GroundChecker>().GroundCheck())
        {
            _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
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
        localforward = localforward.normalized * moveDir.y * moveSpeed;

        changedEuler.y += moveDir.x;
        float checkedCanMove = Mathf.Abs(Vector3.Magnitude(new Vector3(changedVelocity.x, 0, changedVelocity.z) - localforward));
        if (checkedCanMove <= moveSpeed * 2.5f)
            changedVelocity -= localforward;

        transform.eulerAngles = changedEuler;
        _rb.angularVelocity = changedVelocity;
    }
}

using UnityEngine;

namespace _00.Work.MOON._01.Script.Entities
{
    public class temp : MonoBehaviour
    {
        [SerializeField] private float speed = 6f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float maxSlopeAngle = 30f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private Camera mainCamera;
    private bool isGrounded;
    private Vector3 moveDirection;
    private Vector3 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleMovement();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        float horizontal = 0;// = Input.GetAxis("Horizontal");
        float vertical = 0;// = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            Vector3 cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 cameraRight = mainCamera.transform.right;

            moveDirection = (cameraForward * inputDirection.z + cameraRight * inputDirection.x).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            rb.MovePosition(rb.position + moveDirection * (speed * Time.deltaTime));
        }
    }

    private void ApplyGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        velocity.y += gravity * Time.deltaTime;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, velocity.y, rb.linearVelocity.z);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contactCount > 0)
        {
            Vector3 normal = collision.contacts[0].normal;
            float slopeAngle = Vector3.Angle(Vector3.up, normal);

            if (slopeAngle <= maxSlopeAngle)
            {
                Vector3 slopeDirection = Vector3.ProjectOnPlane(moveDirection, normal);
                rb.linearVelocity = slopeDirection * speed;
            }
        }
    }
    }
}
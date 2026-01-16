using UnityEngine;

namespace UnityKing.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        public float walkSpeed = 5f;
        public float sprintSpeed = 8f;
        public float jumpForce = 6f;
        public bool allowJump = true;

        [Header("Ground Check")]
        public Transform groundCheck;
        public float groundRadius = 0.2f;
        public LayerMask groundLayer;

        private Rigidbody rb;
        private Vector3 moveDirection;

        public bool IsGrounded { get; private set; }
        public bool IsMoving => moveDirection.magnitude > 0.1f;
        public float CurrentSpeed { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        public void HandleMovement(Vector2 moveInput, bool jump, bool sprint)
        {
            CheckGround();

            float speed = sprint ? sprintSpeed : walkSpeed;
            CurrentSpeed = speed * moveInput.magnitude;

            moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
            Vector3 velocity = transform.TransformDirection(moveDirection) * speed;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

            if (jump && IsGrounded && allowJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        void CheckGround()
        {
            if (groundCheck == null)
            {
                IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
                return;
            }

            IsGrounded = Physics.CheckSphere(
                groundCheck.position,
                groundRadius,
                groundLayer
            );
        }
    }
}

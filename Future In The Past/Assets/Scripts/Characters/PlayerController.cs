using UnityEngine;

namespace FutureInThePast.Characters
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Physical")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpHeight = 7f;
        [SerializeField] private float superJumpHeight = 14f;
        [SerializeField] private CircleCollider2D groundCheck;

        [Header("Constraints")]
        [SerializeField][Range(0, 1f)] private float moveConstraint = 0.1f;

        private bool canJump;
        private bool superJump;

        private Rigidbody2D rb;

        // Store input values
        private float moveInput;
        private bool jumpInput;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void LateUpdate()
        {
            HandleInput();
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleJump();
        }

        private void HandleInput()
        {
            // Capture movement input
            moveInput = Input.GetAxis("Horizontal");

            // Capture jump input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpInput = true;
            }
        }

        private void HandleMovement()
        {
            if (Mathf.Abs(moveInput) > moveConstraint)
            {
                rb.linearVelocityX = moveInput * moveSpeed;
            }

            // Flip the character sprite based on movement direction
            if (moveInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Flip left
            }
            else if (moveInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Flip right
            }
        }

        private void HandleJump()
        {
            if (jumpInput && canJump)
            {
                Jump();
                // Disable further jumps until grounded
                superJump = false;
                canJump = false; 
            }

            // Reset jump input after processing
            jumpInput = false;
        }

        private void Jump()
        {
            rb.linearVelocityY = superJump ? superJumpHeight : jumpHeight;
        }

        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    // Check if the player is grounded
        //    if (collision.gameObject.CompareTag("Ground"))
        //    {
        //        canJump = true; // Allow jump
        //    }
        //}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                canJump = true;
            }
            if (collision.gameObject.CompareTag("JumpPad"))
            {
                canJump = true;
                superJump = true;
            }
        }
    }
}

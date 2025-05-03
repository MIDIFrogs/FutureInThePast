using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace FutureInThePast.Characters
{
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpHeight = 7f;

        private bool canJump;
        //private bool canDoubleJump;
        private bool isDead = false;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!isDead)
            {
                HandleMovement();
                HandleJump();
            }
        }

        private void HandleMovement()
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.linearVelocityX = moveInput * moveSpeed;

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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canJump)
                {
                    Jump();
                    canJump = false; // Disable further jumps until grounded
                }
                //else if (canDoubleJump)
                //{
                //    Jump();
                //    canDoubleJump = false; // Disable double jump
                //}
            }
        }

        private void Jump()
        {
            rb.linearVelocityY = jumpHeight;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the player is grounded
            if (collision.gameObject.CompareTag("Ground"))
            {
                canJump = true; // Allow jump
                //canDoubleJump = true; // Allow double jump
            }
        }

        public void Die()
        {
            isDead = true;
            // Additional logic for death can be added here
        }

        public bool IsDead()
        {
            return isDead;
        }
    }

}

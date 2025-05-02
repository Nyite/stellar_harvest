using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float jumpForce = 12f;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck; // Assign an empty GameObject at the player's feet
    [SerializeField] LayerMask groundLayer; // Assign the "Ground" layer in the Inspector
    [SerializeField] float groundCheckRadius = 0.2f; // Size of the overlap circle

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jumping
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space)) // Jump input
        {
            if (isGrounded && canJump)
            {
                Jump();
            }
        }

        // Optional: Variable jump height (shorter tap = smaller jump)
        if (Input.GetKeyUp(KeyCode.Space) && (rb.linearVelocity.y > 0))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Instant jump
        // OR for physics-based jump:
        // rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // Visualize ground check (debug)
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}

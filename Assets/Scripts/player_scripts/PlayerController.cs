using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float baseMoveSpeed = 8f;
    [SerializeField] float baseJumpForce = 12f;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck; // Assign an empty GameObject at the player's feet
    [SerializeField] LayerMask groundLayer; // Assign the "Ground" layer in the Inspector
    [SerializeField] float groundCheckRadius = 0.2f; // Size of the overlap circle
    public int _mushroomLayer = 9;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canJump = true;
    private int grassSlowCount = 0; // Tracks overlapping grass objects

    private float currentMoveSpeed;
    private float currentJumpForce;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = baseMoveSpeed;
        currentJumpForce = baseJumpForce;
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * currentMoveSpeed, rb.linearVelocity.y);

        // Jumping
        LayerMask combinedMask = groundLayer | (1 << _mushroomLayer);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, combinedMask);
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
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, currentJumpForce); // Instant jump
        // OR for physics-based jump:
        // rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    // Grass influence on speed process function
    public void ApplySlow(float speedMultiplier, float jumpMultiplier)
    {
        grassSlowCount++;
        currentMoveSpeed = baseMoveSpeed * speedMultiplier;
        currentJumpForce = baseJumpForce * jumpMultiplier;
    }
    public void RemoveSlow(float speedMultiplier, float jumpMultiplier)
    {
        grassSlowCount--;
        if (grassSlowCount <= 0)
        {
            grassSlowCount = 0;
            currentMoveSpeed = baseMoveSpeed;
            currentJumpForce = baseJumpForce;
        }
    }
}

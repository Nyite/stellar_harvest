using UnityEngine;

public class JumpMushroomCtrl : MonoBehaviour
{
    [Header("Settings")]
    public KeyCode interactionKey = KeyCode.E;
    public string requiredTool = "Sickle";

    [Header("Effects")]
    public GameObject destructionEffect;
    public GameObject creationEffect;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] Vector2 bounceDirection = Vector2.up;
    [SerializeField] float minVerticalNormal = 0.7f;

    private bool playerInRange;
    private PlayerInventory playerInventory;
    private PlayerController playerController;
    [SerializeField] float disableTime = 0.5f;
    private Collider2D platformCollider;
    private PlatformEffector2D platformEffector;

    void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        platformEffector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if(playerInRange && Input.GetKeyDown(interactionKey) && playerInventory.hasSickle)
        {
            DestroyMushroom();
        }
    }
    void DestroyMushroom()
    {
        if (destructionEffect != null)
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        playerInventory.hasJumpSeed = true;
        Debug.Log("Jump mushroom was cutted with it seed");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerInventory = other.GetComponent<PlayerInventory>();
            playerController = other.GetComponent<PlayerController>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    System.Collections.IEnumerator DisableCollision()
    {
        platformEffector.enabled = false;  // Disable one-way behavior
        platformCollider.enabled = false;  // Disable collision entirely
        yield return new WaitForSeconds(disableTime);
        platformCollider.enabled = true;
        platformEffector.enabled = true;  // Re-enable one-way behavior
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player touched jump mushroom");
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Debug.Log($"Contact Normal: {contact.normal}, Y-value: {contact.normal.y}");
                if (contact.normal.y >= minVerticalNormal || contact.normal.y <= -minVerticalNormal)
                {
                    Debug.Log("Jump mush was touch upward");
                    Rigidbody2D rb = collision.rigidbody;
                    if (rb != null)
                    {
                        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                        rb.AddForce(bounceDirection * jumpForce, ForceMode2D.Impulse);
                    }
                    break; // Прерываем цикл после первого валидного контакта
                }
            }

        }
    }
}

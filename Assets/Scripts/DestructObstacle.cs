using UnityEngine;

public class DestructObstacle : MonoBehaviour
{
    [Header("Settings")]
    public KeyCode interactionKey = KeyCode.E;
    public string requiredTool = "Sickle";
    public float speedMultiplier = 0.6f; // 50% speed
    public float jumpMultiplier = 0.6f;  // 50% jump height

    [Header("Effects")]
    public GameObject destructionEffect;

    private bool playerInRange;
    private PlayerInventory playerInventory;
    private PlayerController playerController;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey) && playerInventory.hasSickle)
        {
            DestroyObstacle();
        }
    }

    void DestroyObstacle()
    {
        if (destructionEffect != null)
            Instantiate(destructionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerInventory = other.GetComponent<PlayerInventory>();
            playerController = other.GetComponent<PlayerController>();
            playerController.ApplySlow(speedMultiplier, jumpMultiplier);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerInventory = null;
            playerController.RemoveSlow(speedMultiplier, jumpMultiplier);
            playerController = null;
        }
    }
}

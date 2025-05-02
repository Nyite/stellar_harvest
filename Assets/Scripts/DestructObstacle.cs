using UnityEngine;

public class DestructObstacle : MonoBehaviour
{
    [Header("Settings")]
    public KeyCode interactionKey = KeyCode.E;
    public string requiredTool = "Sickle"; // Tool name (matches inventory)

    [Header("Effects")]
    public GameObject destructionEffect;

    private bool playerInRange;
    private PlayerInventory playerInventory;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            TryDestroyObstacle();
        }
    }

    void TryDestroyObstacle()
    {
        // Check if player has the required tool
        if (playerInventory != null && playerInventory.hasSickle) { 
            DestroyObstacle(); 
        }
        else { 
            Debug.Log("You can't get through this growth, and you can't remove it with your bare hands.");
        }
    }

    void DestroyObstacle()
    {
        // Play destruction effect
        if (destructionEffect != null)
            Instantiate(destructionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject); // Remove obstacle
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerInventory = other.GetComponent<PlayerInventory>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerInventory = null;
        }
    }
}

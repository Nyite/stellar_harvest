using UnityEngine;

public class RegularMushroomCtrl : MonoBehaviour
{
    [Header("Settings")]
    public KeyCode interactionKey = KeyCode.E;
    public string requiredTool = "Sickle";

    [Header("Effects")]
    public GameObject destructionEffect;

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
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(DisableCollision());
        }
    }
    void DestroyMushroom()
    {
        if (destructionEffect != null)
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        playerInventory.hasRegSeed = true;
        Debug.Log("Reg mushroom was cutted with it seeds");
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
}

using UnityEngine;

public class PlantAreaInteraction : MonoBehaviour
{
    [Header("Settings")]
    public KeyCode interactionKey = KeyCode.Q;

    [Header("Effects")]
    public GameObject creationEffect;

    private bool playerInRange;
    private bool mushroomPresense;
    private PlayerInventory playerInventory;
    public GameObject RegularMush;
    public GameObject JumpMush;

    void Update()
    {
        if(playerInRange && Input.GetKeyDown(interactionKey))
        {
            Debug.Log("Planting mashroom attempt");
            if (mushroomPresense)
            {
                Debug.Log("Current plant area is locked by other mushroom");
            }
            else 
            {
                if (playerInventory.hasRegSeed)
                {
                    Debug.Log("Reg mush was planted");
                    GameObject instance = Instantiate(RegularMush);
                    instance.transform.position = transform.position;
                    instance.transform.rotation = Quaternion.identity;
                    Debug.Log("Reg mush was planted");
                    playerInventory.hasRegSeed = false;
                }
                else
                {
                    Debug.Log("I need regular mushroom seed for it planting");
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerInventory = other.GetComponent<PlayerInventory>();
        }
        if (other.CompareTag("Mushroom"))
        {
            mushroomPresense = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

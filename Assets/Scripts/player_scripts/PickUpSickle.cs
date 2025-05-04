using UnityEngine;

public class PickUpSickle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInventory>().CollectSickle();
            Destroy(gameObject); // Remove pickup
        }
    }
}

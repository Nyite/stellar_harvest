using UnityEngine;

public class StarCollect : MonoBehaviour
{
    public GameObject destructionEffect;
    private bool playerInRange;
    private PlayerInventory playerInventory;

    void Update()
    {
        if(playerInRange)
        {
            DestroyStar();
        }
    }
    void DestroyStar()
    {
        if (destructionEffect != null)
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        playerInventory.CollectStar();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerInventory = other.GetComponent<PlayerInventory>();
        }
    }
}

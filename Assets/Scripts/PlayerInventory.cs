using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasSickle; // Tracks if player has the required tool

    // Call this when player picks up a sickle
    public void CollectSickle()
    {
        hasSickle = true;
        Debug.Log("Sickle acquired!");
    }
}

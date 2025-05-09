using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasSickle; // Tracks if player has the required tool
    public bool hasRegSeed;
    public bool hasJumpSeed;

    public int numCollectStars = 0; 
    // Call this when player picks up a sickle
    public void CollectSickle()
    {
        hasSickle = true;
        Debug.Log("Sickle acquired!");
    }
    public void CollectJumpSeed()
    {
        hasJumpSeed = true;
        Debug.Log("Jump mushroom seed acquired!");
    }
    public void CollectRegularSeed()
    {
        hasRegSeed = true;
        Debug.Log("Regular mushroom seed acquired!");
    }
    public void CollectStar()
    {
        numCollectStars += 1;
        Debug.Log("You got the star!");
    }
}

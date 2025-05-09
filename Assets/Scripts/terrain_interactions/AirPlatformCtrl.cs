using UnityEngine;

public class AirPlatformCtrl : MonoBehaviour
{
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
        // Press S/DownArrow to drop through
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(DisableCollision());
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
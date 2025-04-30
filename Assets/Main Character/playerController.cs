using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class playerController : MonoBehaviour
{
    public float maxRunSpeed = 2f;
    public float maxAirSpeed = 1f;
    public float groundAccel = 5f;
    public float airAccel = 1f;
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    public float speed = 10f;
    private float movement;
    private float jump;

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, bc2d.size.y / 2 * 10.1f, 0), Vector2.down, 0.01f);
        return hit.collider != null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObject = this.gameObject;
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        Debug.DrawRay(transform.position - new Vector3(0, bc2d.size.y / 2 * 10.1f, 0), Vector2.down, Color.purple, 0.01f);
        jump = Input.GetAxis("Vertical");

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (IsGrounded())
            {
                if (Mathf.Abs(rb2d.linearVelocityX) < maxRunSpeed)
                    rb2d.linearVelocityX += movement * groundAccel * Time.deltaTime;
                else
                    rb2d.linearVelocityX /= 1.01f;
            }
            else
            {
                if (Mathf.Abs(rb2d.linearVelocityX) < maxAirSpeed)
                    rb2d.linearVelocityX += movement * airAccel * Time.deltaTime;
            }
        }
        if(jump > 0)
            if(IsGrounded())
                {
                    rb2d.linearVelocityY = 10;
                }
    }
}

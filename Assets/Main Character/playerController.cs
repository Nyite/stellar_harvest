using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class playerController : MonoBehaviour
{
    private float runSpeed = 6f;
    private float maxRunSpeed = 8f;
    private float maxAirSpeed = 5f;
    private float airAccel = 50f;
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    private float movement;
    private float jump;

    public bool IsGrounded()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position + new Vector3(bc2d.size.x / 2 * transform.localScale.x, -bc2d.size.y / 1.99f * transform.localScale.y, 0), Vector2.down, 0.02f);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(-bc2d.size.x / 2 * transform.localScale.x, -bc2d.size.y / 1.99f * transform.localScale.y, 0), Vector2.down, 0.02f);
        return hit1.collider != null || hit2.collider != null;
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
        Debug.DrawRay(transform.position + new Vector3(bc2d.size.x / 2 * transform.localScale.x, -bc2d.size.y / 1.99f * transform.localScale.y, 0), Vector2.down * 0.02f, Color.purple, 0.01f);
        Debug.DrawRay(transform.position + new Vector3(-bc2d.size.x / 2 * transform.localScale.x, -bc2d.size.y / 1.99f * transform.localScale.y, 0), Vector2.down * 0.02f, Color.purple, 0.01f);
        jump = Input.GetAxis("Vertical");

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (IsGrounded())
            {
                if (Mathf.Abs(rb2d.linearVelocityX) < maxRunSpeed)
                    rb2d.linearVelocityX = movement * runSpeed;
            }
            else
            {
                if (Mathf.Abs(rb2d.linearVelocityX) < maxAirSpeed || rb2d.linearVelocityX * movement < 0)
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

using Unity.Mathematics;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       float movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;

        float jump = Input.GetAxis("Vertical");
        if(jump > 0)
        {
            transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        }
    }
}

using UnityEngine;

public class MovementSub : MonoBehaviour
{
    
    Transform tf;
    Rigidbody2D rb2d;
    float turnSpeed, speed;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        turnSpeed = 135f;
        speed = 500f;
    }

    void Update()
    {
        if (Input.GetKey("d"))
        { tf.Rotate(0, 0, -turnSpeed * Time.deltaTime); }

        if (Input.GetKey("a"))
        { tf.Rotate(0, 0, turnSpeed * Time.deltaTime); }

        if (Input.GetKey(KeyCode.W))
        { rb2d.AddForce(transform.up * speed * Time.deltaTime); }

        if (Input.GetKey(KeyCode.S))
        { rb2d.AddForce(-transform.up * speed * .5f * Time.deltaTime); }

        if (Input.GetKey(KeyCode.G))
        { rb2d.AddForce(transform.right * speed * .5f * Time.deltaTime); }

        if (Input.GetKey(KeyCode.F))
        { rb2d.AddForce(-transform.right * speed * .5f * Time.deltaTime); }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpHeight;
    private float velX, velY;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        Flip();
        Jump();
    }




    // Método para mover al personaje
    public void Movement()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.linearVelocity.y;

        rb.linearVelocity = new Vector2(velX * speed, velY);
    }

    // Método para voltear al personaje según la dirección de movimiento
    public void Flip()
    {
        if (rb.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        else if (rb.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
        }


    }

}





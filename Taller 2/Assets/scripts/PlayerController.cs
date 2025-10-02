using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private float velX, velY;
    private Rigidbody2D rb;
    private Transform groundCheck;
    private bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        Move();
        Flip();
        CheckGrounded();
        Jump();
    }

    // M�todo para mover al personaje
    public void Move()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.linearVelocity.y;

        rb.linearVelocity = new Vector2(velX * speed, velY);
    }

    // M�todo para voltear al personaje seg�n la direcci�n de movimiento
    public void Flip()
    {
        if (rb.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // M�todo para verificar si el personaje est� tocando el suelo
    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    // M�todo para saltar
    public void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}




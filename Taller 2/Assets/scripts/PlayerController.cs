using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce; // fuerza del salto
    private float velX, velY;
    private Rigidbody2D rb;

    private bool isGrounded;
    Animator anim;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Flip();
        Jump();
        

        if (isGrounded) 
        {
            anim.SetBool("jump", false);
        }
        else 
        {
            anim.SetBool("jump", true);
        }
        attack();
    }

    // Movimiento
    public void Move()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.linearVelocity.y;
        rb.linearVelocity = new Vector2(velX * speed, velY);

        if (rb.linearVelocity.x != 0)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

    }

    public void attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("attack", true);
        }
        else
        {
            anim.SetBool("attack", false);
        }
    }




    // Voltear sprite
    public void Flip()
    {
        if (rb.linearVelocity.x > 0)
            transform.localScale = new Vector3(3, 3, 3);
        else if (rb.linearVelocity.x < 0)
            transform.localScale = new Vector3(-3, 3, 3);
    }

    // Salto
    public void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // Detectar si toca el suelo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}

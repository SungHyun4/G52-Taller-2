using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed;
    public float jumpForce;
    private float velX, velY;
    private Rigidbody2D rb;
    private bool isGrounded;

    private Animator anim;

    [Header("Vida del jugador")]
    public float maxHealth = 10f;
    public float currentHealth;

    [Header("Daño enemigo")]
    public float damageCooldown = 1f; // Tiempo entre daños si está en contacto
    private float lastDamageTime = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Move();
        Flip();
        Jump();
        attack();

        if (isGrounded)
            anim.SetBool("jump", false);
        else
            anim.SetBool("jump", true);
    }

    // Movimiento
    public void Move()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.linearVelocity.y;
        rb.linearVelocity = new Vector2(velX * speed, velY);

        anim.SetBool("walk", rb.linearVelocity.x != 0);
    }

    public void attack()
    {
        anim.SetBool("attack", Input.GetButtonDown("Fire1"));
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
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            isGrounded = false;
    }


    // Función para recibir daño
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Jugador recibió daño. Vida actual: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Jugador muerto");
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detectar pinchos
        if (collision.CompareTag("Pinchos"))
        {
            currentHealth = 0f;
            Die();
        }
    }
}


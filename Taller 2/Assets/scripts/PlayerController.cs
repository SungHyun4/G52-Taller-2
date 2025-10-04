using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar la escena GameOver

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed;
    public float jumpForce;
    private float velX, velY;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator anim;

    [Header("Daño enemigo")]
    public float damageCooldown = 1f;
    private float lastDamageTime = 0f;

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
        Attack();
        anim.SetBool("jump", !isGrounded);

        // NUEVO: Verificar vida estrictamente cada frame
        if (GameManager.Instance.vidas <= 0)
        {
            Die();
        }
    }

    public void Move()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.linearVelocity.y;
        rb.linearVelocity = new Vector2(velX * speed, velY);
        anim.SetBool("walk", rb.linearVelocity.x != 0);
    }

    public void Attack()
    {
        anim.SetBool("attack", Input.GetButtonDown("Fire1"));
    }

    public void Flip()
    {
        if (rb.linearVelocity.x > 0)
            transform.localScale = new Vector3(3, 3, 3);
        else if (rb.linearVelocity.x < 0)
            transform.localScale = new Vector3(-3, 3, 3);
    }

    public void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

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

    public void TakeDamage()
    {
        if (Time.time - lastDamageTime < damageCooldown)
            return;

        lastDamageTime = Time.time;
        GameManager.Instance.RestarVida();

        // Ya no es necesario verificar aquí, se hace en Update
    }

    private void Die()
    {
        Debug.Log("Jugador sin vidas -> Game Over");
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);

        // Cargar escena GameOver
        SceneManager.LoadScene("GameOver");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pinchos"))
        {
            GameManager.Instance.vidas = 0;

            if (HUDManager.Instance != null)
                HUDManager.Instance.ActualizarVidas();

            Die();
        }
    }
}

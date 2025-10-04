using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public string enemyName;
    public float healthPoints = 10f;
    public float speed = 2f;
    public float detectionRange = 5f;
    public float damage = 1f; // Da�o que inflige al jugador
    public float damageCooldown = 1f; // Intervalo entre da�os
    private float lastDamageTime = 0f;

    private Animator animator;
    private bool isDead = false;
    private Transform player;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // Buscar al jugador
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("No se encontr� un objeto con tag 'Player'");

        // Posici�n neutral mirando izquierda
        transform.localScale = new Vector3(-4f, 3f, 1f);
    }

    private void Update()
    {
        if (!isDead)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            // Mover enemigo
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            // Voltear seg�n posici�n del jugador
            Flip(direction.x);
        }
    }

    private void Flip(float horizontalDirection)
    {
        if (horizontalDirection > 0)
            transform.localScale = new Vector3(-4f, 3f, 1f);  // Mirando derecha
        else if (horizontalDirection < 0)
            transform.localScale = new Vector3(4f, 3f, 1f); // Mirando izquierda (neutral)
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDead) return;

        // Da�o por arma
        if (collision.CompareTag("Weapon"))
        {
            Weapon weapon = collision.GetComponent<Weapon>();
            if (weapon != null)
            {
                healthPoints -= weapon.damage;
                if (healthPoints <= 0)
                    Die();
            }
        }

        // Da�o al jugador
        if (collision.CompareTag("Player") && Time.time - lastDamageTime >= damageCooldown)
        {
            GameManager.Instance.RestarVida();
            lastDamageTime = Time.time;
        }
    }

    private void Die()
    {
        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("death");
            Destroy(gameObject, 1f); // Duraci�n de la animaci�n
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

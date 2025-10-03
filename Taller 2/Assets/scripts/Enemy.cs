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

    // Funci�n para voltear enemigo
    private void Flip(float horizontalDirection)
    {
        if (horizontalDirection > 0)
            transform.localScale = new Vector3(-4f, 3f, 1f);  // Mirando derecha
        else if (horizontalDirection < 0)
            transform.localScale = new Vector3(4f, 3f, 1f); // Mirando izquierda (neutral)
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && !isDead)
        {
            Weapon weapon = collision.GetComponent<Weapon>();
            if (weapon != null)
            {
                healthPoints -= weapon.damage;
                if (healthPoints <= 0)
                {
                    Die();
                }
            }
        }

        if (collision.CompareTag("Player") && !isDead && Time.time - lastDamageTime >= damageCooldown)
        {
            Debug.Log("Enemigo hizo da�o al jugador"); //prueba
            PlayerController playerCtrl = collision.GetComponent<PlayerController>();
            if (playerCtrl != null)
            {
                playerCtrl.TakeDamage(damage);
                lastDamageTime = Time.time;
            }
        }
    }


    private void Die()
    {
        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("death");
            Destroy(gameObject, 1f); // Aqu� pones el tiempo que dura tu animaci�n en segundos
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
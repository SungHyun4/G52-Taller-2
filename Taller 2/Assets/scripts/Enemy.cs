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
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("No se encontr� un objeto con tag 'Player'");
        }

        // Fijar tama�o inicial
        transform.localScale = new Vector3(4f, 3f, 1f);
    }

    private void Update()
    {
        // Mantener tama�o fijo
        transform.localScale = new Vector3(4f, 3f, 1f);

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
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            // Girar seg�n direcci�n
            if (direction.x > 0)
                transform.localScale = new Vector3(4f, 3f, 1f);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-4f, 3f, 1f);
        }
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
        animator.SetTrigger("death");

        // Destruir objeto despu�s de animaci�n
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        float deathAnimLength = 1f;
        if (clipInfo.Length > 0)
        {
            deathAnimLength = clipInfo[0].clip.length;
        }

        Destroy(gameObject, deathAnimLength);
    }
}

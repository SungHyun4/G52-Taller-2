using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public string enemyName;
    public float healthPoints;
    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cuando el enemigo recibe daño
        if (collision.CompareTag("Weapon"))
        {
            healthPoints -= 2f; // Resta 2 de vida por golpe

            if (healthPoints <= 0)
            {
                Destroy(gameObject); // Elimina al enemigo
            }
        }
    }
}

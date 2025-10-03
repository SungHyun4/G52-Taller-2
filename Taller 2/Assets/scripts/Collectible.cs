using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { MonedaBronce, GemaRoja, GemaVerde }
    public CollectibleType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"El jugador recogió: {type}");

            // Avisar al GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddCollectible(type);
            }

            Destroy(gameObject);
        }
    }
}



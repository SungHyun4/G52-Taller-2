using UnityEngine;

public class MetaControllerSeguro : MonoBehaviour
{
    private FinalEstadisticas finalEstadisticas;

    private void Awake()
    {
        // Buscar automáticamente el FinalEstadisticas en la escena usando método actualizado
        finalEstadisticas = GameObject.FindFirstObjectByType<FinalEstadisticas>();
        if (finalEstadisticas == null)
            Debug.LogError("No se encontró FinalEstadisticas en la escena");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisión detectada: " + collision.name + " Tag: " + collision.tag);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador tocó el pergamino");
            finalEstadisticas.ActivarEstadisticas();
        }
    }
}


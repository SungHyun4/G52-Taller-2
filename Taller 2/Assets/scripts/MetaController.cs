using UnityEngine;

public class MetaControllerSeguro : MonoBehaviour
{
    private FinalEstadisticas finalEstadisticas;

    private void Awake()
    {
        // Buscar autom�ticamente el FinalEstadisticas en la escena usando m�todo actualizado
        finalEstadisticas = GameObject.FindFirstObjectByType<FinalEstadisticas>();
        if (finalEstadisticas == null)
            Debug.LogError("No se encontr� FinalEstadisticas en la escena");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisi�n detectada: " + collision.name + " Tag: " + collision.tag);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador toc� el pergamino");
            finalEstadisticas.ActivarEstadisticas();
        }
    }
}


using UnityEngine;
using TMPro;

public class FinalEstadisticas : MonoBehaviour
{
    private TMP_Text GemaVerdeDef;
    private TMP_Text GemaRojaDef;
    private TMP_Text MonedaDef;
    private TMP_Text TiempoDef;
    private GameObject panelEstadisticasCanvas;

    private void Awake()
    {
        // Buscar el Canvas automáticamente
        panelEstadisticasCanvas = GameObject.Find("Estadisticas");
        if (panelEstadisticasCanvas == null)
        {
            Debug.LogError("No se encontró el Canvas 'Estadisticas'");
            return;
        }

        // Buscar los TMP automáticamente por nombre
        GemaVerdeDef = BuscarTMP("Gema verde def");
        GemaRojaDef = BuscarTMP("Gema roja def");
        MonedaDef = BuscarTMP("Moneda def");
        TiempoDef = BuscarTMP("Tiempo def");

        // Ocultarlo al inicio
        panelEstadisticasCanvas.SetActive(false);
    }

    private TMP_Text BuscarTMP(string nombre)
    {
        GameObject obj = GameObject.Find(nombre);
        if (obj != null)
            return obj.GetComponent<TMP_Text>();
        else
            Debug.LogWarning("No se encontró TMP con nombre: " + nombre);
        return null;
    }

    // Función pública para activar el panel y mostrar estadísticas
    public void ActivarEstadisticas()
    {
        if (panelEstadisticasCanvas != null)
        {
            panelEstadisticasCanvas.SetActive(true);
            panelEstadisticasCanvas.transform.SetAsLastSibling(); // Traer al frente
        }

        if (MonedaDef != null)
            MonedaDef.text = "Monedas: " + GameManager.Instance.monedasBronce;

        if (GemaVerdeDef != null)
            GemaVerdeDef.text = "Gemas verdes: " + GameManager.Instance.gemasVerdes;

        if (GemaRojaDef != null)
            GemaRojaDef.text = "Gemas rojas: " + GameManager.Instance.gemasRojas;

        if (TiempoDef != null)
            TiempoDef.text = "Tiempo: " + GameManager.Instance.GlobaltimeTotal.ToString("F2") + "s";

        // Pausar juego
        Time.timeScale = 0f;

        Debug.Log("Panel de estadísticas activado, juego pausado");
    }

    // Función para cerrar estadísticas y reanudar juego si quieres
    public void CerrarEstadisticas()
    {
        if (panelEstadisticasCanvas != null)
            panelEstadisticasCanvas.SetActive(false);

        Time.timeScale = 1f; // Reanuda juego
    }
}

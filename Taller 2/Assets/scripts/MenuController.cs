using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Panel de instrucciones
    public GameObject panelInstrucciones;

    // Función para el botón Jugar
    public void Jugar()
    {
        SceneManager.LoadScene("Scene 1"); // Cambia "Escena1" por tu escena 1
    }

    // Función para mostrar el panel de instrucciones
    public void MostrarInstrucciones()
    {
        if (panelInstrucciones != null)
        {
            panelInstrucciones.SetActive(true);
        }
    }

    // Función para cerrar el panel de instrucciones
    public void CerrarInstrucciones()
    {
        if (panelInstrucciones != null)
        {
            panelInstrucciones.SetActive(false);
        }
    }
}

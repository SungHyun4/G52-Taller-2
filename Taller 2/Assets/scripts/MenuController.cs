using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Panel de instrucciones
    public GameObject panelInstrucciones;

    // Funci�n para el bot�n Jugar
    public void Jugar()
    {
        SceneManager.LoadScene("Scene 1"); // Cambia "Escena1" por tu escena 1
    }

    // Funci�n para mostrar el panel de instrucciones
    public void MostrarInstrucciones()
    {
        if (panelInstrucciones != null)
        {
            panelInstrucciones.SetActive(true);
        }
    }

    // Funci�n para cerrar el panel de instrucciones
    public void CerrarInstrucciones()
    {
        if (panelInstrucciones != null)
        {
            panelInstrucciones.SetActive(false);
        }
    }
}

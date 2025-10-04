using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void Reintentar()
    {
        SceneManager.LoadScene("Scene 1"); // tu escena de juego
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("Inicio"); // tu escena de inicio
    }
}

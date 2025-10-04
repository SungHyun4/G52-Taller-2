using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Tiempo global de juego")]
    public float GlobaltimeTotal = 0f;

    [Header("Coleccionables")]
    public int monedasBronce = 0;
    public int gemasRojas = 0;
    public int gemasVerdes = 0;

    [Header("Jugador - Vidas")]
    public int maxVidas = 10;
    public int vidas = 10;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Contador de tiempo global
        GlobaltimeTotal += Time.deltaTime;
    }

    // -------------------------------
    // Función para coleccionables
    // -------------------------------
    public void AddCollectible(Collectible.CollectibleType type)
    {
        switch (type)
        {
            case Collectible.CollectibleType.MonedaBronce:
                monedasBronce++;
                break;
            case Collectible.CollectibleType.GemaRoja:
                gemasRojas++;
                break;
            case Collectible.CollectibleType.GemaVerde:
                gemasVerdes++;
                break;
        }

        if (HUDManager.Instance != null)
            HUDManager.Instance.ActualizarHUD();
    }

    // -------------------------------
    // Funciones para manejar vidas
    // -------------------------------
    public void RestarVida()
    {
        if (vidas > 0)
        {
            vidas--;
            if (HUDManager.Instance != null)
                HUDManager.Instance.ActualizarVidas();
        }
    }

    public void SumarVida()
    {
        if (vidas < maxVidas)
        {
            vidas++;
            if (HUDManager.Instance != null)
                HUDManager.Instance.ActualizarVidas();
        }
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float Globaltime = 0;
    public float Globaltime1 { get => Globaltime; set => Globaltime = value; }

    [Header("Coleccionables")]
    public int monedasBronce = 0;
    public int gemasRojas = 0;
    public int gemasVerdes = 0;

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

    public void SumaTimeGlobal(float timeScene)
    {
        Globaltime += timeScene;
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
                Debug.Log($"Jugador recogió una Moneda de Bronce. Total: {monedasBronce}");
                break;

            case Collectible.CollectibleType.GemaRoja:
                gemasRojas++;
                Debug.Log($"Jugador recogió una Gema Roja. Total: {gemasRojas}");
                break;

            case Collectible.CollectibleType.GemaVerde:
                gemasVerdes++;
                Debug.Log($"Jugador recogió una Gema Verde. Total: {gemasVerdes}");
                break;
        }
    }
}

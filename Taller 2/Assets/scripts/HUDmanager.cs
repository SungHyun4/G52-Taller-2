using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("Textos HUD")]
    public TMP_Text TextoMoneda;
    public TMP_Text TextoGemaRoja;
    public TMP_Text TextoGemaVerde;
    public TMP_Text TextoVidas;

    [Header("Textos Timer")]
    public TMP_Text TextoTimerMin;
    public TMP_Text TextoTimerSec;
    public TMP_Text TextoTimerCenti;

    private float startTimeScene;   // Tiempo al iniciar la escena
    private float elapsedSceneTime; // Tiempo transcurrido en la escena
    private bool timerRunning = true;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ActivarTextos();
        ActualizarHUD();
        startTimeScene = Time.time;
        timerRunning = true;
    }

    private void Update()
    {
        ActualizarHUD();
        if (timerRunning)
            ActualizarTimer();
    }

    private void ActivarTextos()
    {
        if (TextoVidas != null) TextoVidas.gameObject.SetActive(true);
        if (TextoMoneda != null) TextoMoneda.gameObject.SetActive(true);
        if (TextoGemaRoja != null) TextoGemaRoja.gameObject.SetActive(true);
        if (TextoGemaVerde != null) TextoGemaVerde.gameObject.SetActive(true);
        if (TextoTimerMin != null) TextoTimerMin.gameObject.SetActive(true);
        if (TextoTimerSec != null) TextoTimerSec.gameObject.SetActive(true);
        if (TextoTimerCenti != null) TextoTimerCenti.gameObject.SetActive(true);
    }

    /// <summary>
    /// Actualiza HUD completo (vidas y coleccionables)
    /// </summary>
    public void ActualizarHUD()
    {
        ActualizarVidas();
        ActualizarColeccionables();
    }

    private void ActualizarColeccionables()
    {
        if (GameManager.Instance == null) return;
        if (TextoMoneda != null) TextoMoneda.text = GameManager.Instance.monedasBronce.ToString();
        if (TextoGemaRoja != null) TextoGemaRoja.text = GameManager.Instance.gemasRojas.ToString();
        if (TextoGemaVerde != null) TextoGemaVerde.text = GameManager.Instance.gemasVerdes.ToString();
    }

    public void ActualizarVidas()
    {
        if (TextoVidas != null && GameManager.Instance != null)
            TextoVidas.text = GameManager.Instance.vidas.ToString();
    }

    private void ActualizarTimer()
    {
        elapsedSceneTime = Time.time - startTimeScene;
        int minutes = (int)elapsedSceneTime / 60;
        int seconds = (int)elapsedSceneTime % 60;
        int centi = (int)((elapsedSceneTime - (seconds + minutes * 60)) * 100);

        if (TextoTimerMin != null) TextoTimerMin.text = (minutes < 10 ? "0" : "") + minutes;
        if (TextoTimerSec != null) TextoTimerSec.text = (seconds < 10 ? "0" : "") + seconds;
        if (TextoTimerCenti != null) TextoTimerCenti.text = (centi < 10 ? "0" : "") + centi;
    }

    /// <summary>
    /// Llamar al finalizar la escena para sumar tiempo al total del juego
    /// </summary>
    public void GuardarTiempoEscena()
    {
        timerRunning = false;
        if (GameManager.Instance != null)
            GameManager.Instance.GlobaltimeTotal += elapsedSceneTime;
    }

    /// <summary>
    /// Reiniciar timer para la nueva escena
    /// </summary>
    public void ReiniciarTimerEscena()
    {
        startTimeScene = Time.time;
        elapsedSceneTime = 0;
        timerRunning = true;
    }
}

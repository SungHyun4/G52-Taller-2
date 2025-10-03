using UnityEngine;
using TMPro; // Usamos TextMeshPro

public class HUDManager : MonoBehaviour
{
    public TMP_Text TextoMoneda;
    public TMP_Text TextoGemaRoja;
    public TMP_Text TextoGemaVerde;

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            TextoMoneda.text = GameManager.Instance.monedasBronce.ToString();
            TextoGemaRoja.text = GameManager.Instance.gemasRojas.ToString();
            TextoGemaVerde.text = GameManager.Instance.gemasVerdes.ToString();
        }
    }
}

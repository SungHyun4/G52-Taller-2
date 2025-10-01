using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private float Globaltime = 0;

    public float Globaltime1 { get => Globaltime; set => Globaltime = value; }

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


// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SumaTimeGlobal(float timeScene)
    {
       
    }
}

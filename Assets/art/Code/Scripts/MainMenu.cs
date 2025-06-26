using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject easyMode;
    [SerializeField] private GameObject mediumMode;
    [SerializeField] private GameObject hardMode;
    public static MainMenu Instance;

    public enum Difficolta { Facile, Medio, Difficile }
    public Difficolta difficoltaScelta = Difficolta.Facile;

    private void Awake()
    {
        // Singleton: ne esiste solo uno
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // non distruggere cambiando scena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
       easyMode.SetActive(true); 
    }
    public void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene(1);
    }

    public void SelezionaFacile()
    {
        MainMenu.Instance.difficoltaScelta = MainMenu.Difficolta.Facile;
        easyMode.SetActive(true);
        mediumMode.SetActive(false);
        hardMode.SetActive(false);
    }

    public void SelezionaMedio()
    {
        MainMenu.Instance.difficoltaScelta = MainMenu.Difficolta.Medio;
        easyMode.SetActive(false);
        mediumMode.SetActive(true);
        hardMode.SetActive(false);
    }

    public void SelezionaDifficile()
    {
        MainMenu.Instance.difficoltaScelta = MainMenu.Difficolta.Difficile;
        easyMode.SetActive(false);
        mediumMode.SetActive(false);
        hardMode.SetActive(true);
    }
}


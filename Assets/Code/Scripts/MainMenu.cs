using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

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
    public void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene(1);
    }

    public void SelezionaFacile()
    {
        MainMenu.Instance.difficoltaScelta = MainMenu.Difficolta.Facile;
        SceneManager.LoadScene("ScenaGioco");
    }

    public void SelezionaMedio()
    {
        MainMenu.Instance.difficoltaScelta = MainMenu.Difficolta.Medio;
        SceneManager.LoadScene("ScenaGioco");
    }

    public void SelezionaDifficile()
    {
        MainMenu.Instance.difficoltaScelta = MainMenu.Difficolta.Difficile;
        SceneManager.LoadScene("ScenaGioco");
    }
}


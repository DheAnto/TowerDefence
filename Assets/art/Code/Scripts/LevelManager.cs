using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Collections;


public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI textHP;
    [SerializeField] TextMeshProUGUI textWave;
    [SerializeField] TextMeshProUGUI textDifficulty;

    [Header("Attributes")]
    public int healthPoints;
    public int waveToWin;

    public String difficultyLevel;

    public static LevelManager main;

    public int currentWave = 1;
    
    public List<GameObject> pathPoints = new List<GameObject>();
    public GameObject[] path;

    public int currency;

    private void Awake()
    {
        main=this;   
    }
    private void Start()
    {
        var difficolta = MainMenu.Instance.difficoltaScelta;

        switch (difficolta)
        {
            case MainMenu.Difficolta.Facile:
                healthPoints = 5;
                difficultyLevel = "Easy";
                break;
            case MainMenu.Difficolta.Medio:
                healthPoints = 3;
                difficultyLevel = "Medium";
                break;
            case MainMenu.Difficolta.Difficile:
                healthPoints = 1;
                difficultyLevel = "Hard";
                break;
        }
    
}
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            //BUY ITEM
            currency -= amount;
            return true;
        }
        else {
            Debug.Log("Not enough currency");
            return false;
        }
    }

    public void DecreaseHealth(int amount)
    {
        healthPoints -= amount;
        Debug.Log("Health: " + healthPoints);
        if (healthPoints <= 0)
        {
            //GAME OVER
            WinLosePanelManager.main.ShowLosingPanel();
            Invoke("LoadMainMenu", 5f);
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    

    public void addPoint(GameObject Object)
    {
        pathPoints.Add(Object);
        Debug.Log("point added");
    }

    private void OnGUI()
    {
        textHP.text = "HP = " + LevelManager.main.healthPoints;
        textWave.text = "Wave = " + LevelManager.main.currentWave + "/" + waveToWin;
        textDifficulty.text = "Difficulty level = " + difficultyLevel;

    }

    public void transferListToArray(){
        path = pathPoints.ToArray();
    }

    


}

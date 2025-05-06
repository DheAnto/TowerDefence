using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;


public class NuovoLevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI textHP;
    [SerializeField] TextMeshProUGUI textWave;


    [Header("Attributes")]
    public int healthPoints;
    public int waveToWin;

    public static NuovoLevelManager main;

    public int currentWave = 1;
    
    public GameObject startPoint;
    public List<GameObject> pathPoints = new List<GameObject>();

    public int currency;
    private void Awake()
    {
        main=this;   
    }
    
    private void Start()
    {
        currency = currency;
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
        }
    }

    public void addPoint(GameObject Object){
        pathPoints.Add(Object);
        Debug.Log("point added");
    }

    private void OnGUI()
    {
        textHP.text = "HP = " + LevelManager.main.healthPoints;
        textWave.text = "Wave = " + LevelManager.main.currentWave;
    } 


}

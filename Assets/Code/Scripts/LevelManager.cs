using TMPro;
using UnityEditor.SearchService;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI textHP;
    [SerializeField] TextMeshProUGUI textWave;

    [Header("Attributes")]
    public int healthPoints;
    public int waveToWin;

    public static LevelManager main;

    public int currentWave = 1;
    
    public Transform startPoint;
    public Transform[] path;

    public int currency;
    private void Awake()
    {
        main=this;   
    }
    
    //private void Start()
    //{
      //  currency = currency;
    //}
    
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

    private void OnGUI()
    {
        textHP.text = "HP = " + LevelManager.main.healthPoints;
        textWave.text = "Wave = " + LevelManager.main.currentWave;
    }

    


}

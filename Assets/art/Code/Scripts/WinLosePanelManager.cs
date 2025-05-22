using UnityEngine;

public class WinLosePanelManager : MonoBehaviour
{
    public static WinLosePanelManager main;

    [Header("References")]
    [SerializeField] public GameObject VictoryPanel;
    [SerializeField] public GameObject LosingPanel;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        VictoryPanel.SetActive(false);
        LosingPanel.SetActive(false);
    }

    public void ShowVictoryPanel()
    {
        VictoryPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowLosingPanel()
    {
        LosingPanel.SetActive(true);
        Time.timeScale = 0;
    }
}



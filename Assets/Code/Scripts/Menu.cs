using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI costTorreCiociaUI;
    [SerializeField] TextMeshProUGUI costTorreAntoUI;
    [SerializeField] TextMeshProUGUI costTorreNiccoUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }
    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        costTorreCiociaUI.text = "Cost = " + BuildManager.main.GetTower(0).getCost();
        costTorreAntoUI.text = "Cost = " + BuildManager.main.GetTower(1).getCost();
        costTorreNiccoUI.text = "Cost = " + BuildManager.main.GetTower(2).getCost();
    }   
}


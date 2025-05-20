using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] private Tower TorreCiocia;
    [SerializeField] private Tower TorreAnto;
    [SerializeField] private Tower TorreNicco;
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
        costTorreCiociaUI.text = "Cost = " + TorreCiocia.cost;
        costTorreAntoUI.text = "Cost = " + TorreAnto.cost;
        costTorreNiccoUI.text = "Cost = " + TorreNicco.cost;
    }   
}


using System.Runtime.CompilerServices;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;
    [SerializeField] private GameObject turretCiociaButton;
    [SerializeField] private GameObject turretAntoButton;
    [SerializeField] private GameObject turretNiccoButton;




    private int selectedTower = 0;

    private void Awake()
    {
        main = this;
    }

    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    private void Start()
    {
        turretCiociaButton.SetActive(true);
    }

    public void setSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
        switch (_selectedTower)
        {
            case 0:
                pickedCiociaTower();
                break;
            case 1:
                pickedAntoTower();
                break;
            case 2:
                pickedNiccoTower();
                break;
        }
    }

    public Tower GetTower(int index)
    {
        if (index < 0 || index >= towers.Length)
        {
            Debug.LogError("Tower index out of range");
            return null;
        }
        return towers[index];
    }

    private void pickedCiociaTower()
    {
        turretCiociaButton.SetActive(true);
        turretAntoButton.SetActive(false);
        turretNiccoButton.SetActive(false);
    }

    private void pickedAntoTower()
    {
        turretCiociaButton.SetActive(false);
        turretAntoButton.SetActive(true);
        turretNiccoButton.SetActive(false);
    }

    private void pickedNiccoTower()
    {
        turretCiociaButton.SetActive(false);
        turretAntoButton.SetActive(false);
        turretNiccoButton.SetActive(true);
    }

}

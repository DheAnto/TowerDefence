using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int selectedTower = 0;

    private void Awake()
    {
        main = this;
    }

     public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void setSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
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
}

using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject towerObj;
    public TurretScript turret;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
       
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        if (UIManager.main.IsHoveringUI()) return;
        
        if (towerObj != null)
        {
            turret.OpenUpgradeUI();
            return;
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        if (!LevelManager.main.SpendCurrency(towerToBuild.cost))
        {
            Debug.Log("You can't afford that!");
            return;
        }
        towerObj = Instantiate(towerToBuild.prefab , transform.position, Quaternion.identity);
        turret = towerObj.GetComponent<TurretScript>();
    }
}

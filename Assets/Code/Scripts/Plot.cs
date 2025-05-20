using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{

    [Header("References")]
    
    [SerializeField] private Color hoverColor;
    [SerializeField] private Transform turretSpawnPointOnTile;

    private SpriteRenderer sr;
    private GameObject towerObj;
    private TurretScript turret;
    private Color startColor;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
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
        towerObj = Instantiate(towerToBuild.prefab, turretSpawnPointOnTile.position, Quaternion.identity);
        turret = towerObj.GetComponent<TurretScript>();
    }
}

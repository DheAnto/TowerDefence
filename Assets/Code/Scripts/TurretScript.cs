using UnityEngine;
using UnityEditor;
using Unity.Mathematics;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class TurretScript : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] TextMeshProUGUI upgradeCostViewed;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps = 1f; //bullets per seconds
    [SerializeField] private int upgradeCost = 100;
    [SerializeField] private float upgradingCostMoltiplicator = 1.0f;


    private float bpsBase;
    private float targetingRangeBase;
    private int level = 1;

    private Transform target;
    private float timeUntilFire;

    private void Start()
    {
        bpsBase = bps;
        targetingRangeBase = targetingRange;

        upgradeButton.onClick.AddListener(Upgrade);
        upgradeCostViewed.text = "" + upgradeCost;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
    // Update is called once per frame
    void Update()
    {
        if(target == null){
            FindTarget();
            return;
        }
        RotateTowardsTarget();
        
        if (!checkTargetIsInRange())
        {
            target = null;
            return;
        } else {
            timeUntilFire += Time.deltaTime;
            

            if (timeUntilFire >= 1f / bps){
                
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot(){
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, quaternion.identity);
        BulletScript bulletScript = bulletObj.GetComponent<BulletScript>();
        bulletScript.SetTarget(target);
    }

    private void RotateTowardsTarget(){
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) 
        * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void FindTarget(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0){
            target = hits[0].transform;
        }        

    }

    public void OpenUpgradeUI()
    {
        upgradeCostViewed.text = "" + upgradeCost;
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }
    public void Upgrade()
    {
        if (upgradeCost > LevelManager.main.currency) return;

        LevelManager.main.SpendCurrency(upgradeCost);

        level++;

        bps = CalculateBPS();

        targetingRange = CalculateInRange();
        CloseUpgradeUI();
        Debug.Log("new bps" + bps);
        Debug.Log("new cost" + CalculateCost());
        Debug.Log("new range" + targetingRange);

        upgradeCost = CalculateCost();
        
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(upgradeCost *  Mathf.Pow(level , upgradingCostMoltiplicator));
    }

    private float CalculateBPS()
    {
        return bpsBase * Mathf.Pow(level, 0.6f);
    }

    private float CalculateInRange()
    {
        return targetingRangeBase * Mathf.Pow(level, 0.4f);
    }
    private bool checkTargetIsInRange(){
        return Vector2.Distance(transform.position, target.position) <= targetingRange;
    }
}

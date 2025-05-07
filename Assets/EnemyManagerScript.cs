using UnityEngine;

public class EnemyManagerScript : MonoBehaviour
{
    [SerializeField] public GameObject enemyManager;

    private void Awake()
    {
        enemyManager.SetActive(false);
    }
}



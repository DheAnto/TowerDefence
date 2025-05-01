using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    
    [Header("Attributes")]
    [SerializeField] private GameObject[] enemyPrefabs;


    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5.0f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 10f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private float timeSinceLastSpawn;
    private int enemiesAlive = 0;
    private int enemiesLeftToSpawn;
    private Boolean isSpawning = false;
    private float eps;
    private void Start()
    {   
        enabled = false;
        StartCoroutine(StartWave());
    }

    public void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);   
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning) return;
            
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            spawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0){
            EndWave();
        }
    }

    private void EndWave(){
        if (LevelManager.main.currentWave == LevelManager.main.waveToWin)
        {
            WinLosePanelManager.main.ShowVictoryPanel();
            return;
        }
        LevelManager.main.currentWave++;
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        StartCoroutine(StartWave());

    }
    
    private void EnemyDestroyed(){
        enemiesAlive--;
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }
    private void spawnEnemy()
    {
        int index = UnityEngine.Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.pathPoints[0].gameObject.transform.position, Quaternion.identity);
    }
    private int EnemiesPerWave() {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(LevelManager.main.currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp((enemiesPerSecond * Mathf.Pow(LevelManager.main.currentWave, difficultyScalingFactor)),0f,enemiesPerSecondCap);
    }

}
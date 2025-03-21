using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [Header("Attributes")]
    [SerializeField] private GameObject[] enemyPrefabs;


    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5.0f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive = 0;
    private int enemiesLeftToSpawn;
    private Boolean isSpawning = false;
    private void Start()
    {
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning) return;
            
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            Debug.Log("enemy spawned");
            spawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
    }
    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        }
    private void spawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }
   private int EnemiesPerWave() {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
        }

}

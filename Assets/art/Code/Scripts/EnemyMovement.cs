using System;
using System.Threading;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [Header("Attributes")] 
    [SerializeField] private float  moveSpeed = 2f;

    private GameObject target;
    private int pathIndex = 0;
    private void Start()
    { 
        target = LevelManager.main.path[pathIndex];
    }

    // Update is called once per frame
    private void Update()
    {   
        if(Vector2.Distance(target.transform.position, transform.position) <= 0.1f){
            pathIndex++;

            if(pathIndex == LevelManager.main.path.Length){
                Debug.Log("path length = " + LevelManager.main.path.Length);
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                LevelManager.main.DecreaseHealth(1);
                return;
            } else {
                target = LevelManager.main.path[pathIndex];
            }
        }   
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }
}

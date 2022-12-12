using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyBurstSpawnArea : MonoBehaviour
{
    private Collider[] spawnColliders;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] List<EnemyScriptableObject> enemies = new List<EnemyScriptableObject>();
    [SerializeField] private EnemySpawner.SpawnMethod spawnMethod = EnemySpawner.SpawnMethod.Random;
    [SerializeField] int spawnCount = 10;
    [SerializeField] float spawnDelay = 0.5f;

    private Coroutine SpawnEnemiesCoroutine;
    private Bounds spawnBounds;

    private void Awake()
    {
        if(transform.childCount > 0 && gameObject.GetComponentInChildren<Collider>() != null)
        {
            spawnColliders = gameObject.GetComponentsInChildren<Collider>();
        }
        
        if(spawnColliders == null)
        {
            spawnColliders[0] = GetComponent<Collider>();
        }


        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(SpawnEnemiesCoroutine == null)
        {
            SpawnEnemiesCoroutine = StartCoroutine(SpawnEnemies());
        }
    }

    private Vector3 GetRandomPositionInBounds()
    {
        Vector3 randomBounds;
        int i = Random.Range(1, spawnColliders.Count());
        spawnBounds = spawnColliders[i].bounds;
        randomBounds = new Vector3(Random.Range(spawnBounds.min.x, spawnBounds.max.x), spawnBounds.min.y, Random.Range(spawnBounds.min.z, spawnBounds.max.z));
        return randomBounds;
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        for(int i = 0; i < spawnCount; i++)
        {
            if(spawnMethod == EnemySpawner.SpawnMethod.RoundRobin)
            {
                enemySpawner.DoSpawnEnemy(
                    enemySpawner.weightedEnemies.FindIndex((enemy) => enemy.enemy.Equals(enemies[i % enemies.Count])), 
                    GetRandomPositionInBounds());
            }
            else if(spawnMethod == EnemySpawner.SpawnMethod.Random)
            {
                int index = Random.Range(0, enemies.Count);
                enemySpawner.DoSpawnEnemy(enemySpawner.weightedEnemies.FindIndex((enemy) => enemy.enemy.Equals(enemies[index])), GetRandomPositionInBounds());
            }

            yield return wait;
        }

        Destroy(gameObject);
    }

}

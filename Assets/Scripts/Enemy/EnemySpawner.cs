using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public int numOfEnemiesToSpawn;
    public float spawnDelay;
    public List<EnemyScriptableObject> enemies = new List<EnemyScriptableObject>();
    public SpawnMethod enemySpawnMethod =  SpawnMethod.RoundRobin;

    private NavMeshTriangulation triangulation;
    private Dictionary<int, ObjectPool> enemyObjectPools = new Dictionary<int, ObjectPool>();

    private void Awake()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            enemyObjectPools.Add(i, ObjectPool.CreateInstance(enemies[i].prefab, numOfEnemiesToSpawn));
        }
    }

    private void Start()
    {
        triangulation = NavMesh.CalculateTriangulation();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        int spawnedEnemies = 0;

        while(spawnedEnemies < numOfEnemiesToSpawn)
        {
            if(enemySpawnMethod == SpawnMethod.RoundRobin)
            {
                SpawnRoundRobinEnemy(spawnedEnemies);
            }
            else if(enemySpawnMethod == SpawnMethod.Random)
            {
                SpawnRandomEnemy();
            }

            spawnedEnemies++;

            yield return wait;
        }
    }

    private void SpawnRoundRobinEnemy(int spawnedEnemies)
    {
        int spawnIndex = spawnedEnemies % enemies.Count;

        DoSpawnEnemy(spawnIndex);
    }

    private void SpawnRandomEnemy()
    {
        DoSpawnEnemy(Random.Range(0, enemies.Count));
    }

    public void DoSpawnEnemy(int spawnIndex)
    {
        PoolableObject poolableObject = enemyObjectPools[spawnIndex].GetObject();

        if(poolableObject != null)
        {
            EnemyBase enemy = poolableObject.GetComponent<EnemyBase>();
            enemies[spawnIndex].SetUpEnemy(enemy);

            int vertexIndex = Random.Range(0, triangulation.vertices.Length);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2f, -1))
            {
                enemy.agent.Warp(hit.position);
                enemy.movement.target = player;
                enemy.agent.enabled = true;
                enemy.movement.StartChasing();
            }
            else
            {
                Debug.LogError($"Unable to place NavMeshAgent on NavMesh. Tried to use {triangulation.vertices[vertexIndex]}.");
            }
        }
        else
        {
            Debug.LogError($"Unable to fetch enemy of type {spawnIndex} from object pool. Out of objects?");
        }
    }

    public enum SpawnMethod
    {
        RoundRobin,
        Random
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public int numOfEnemiesToSpawn;
    public float spawnDelay;
    public List<WeightedSpawnScriptableObject> weightedEnemies = new List<WeightedSpawnScriptableObject>();
    public SpawnMethod enemySpawnMethod =  SpawnMethod.RoundRobin;
    [SerializeField] private float[] weights;

    private NavMeshTriangulation triangulation;
    private Dictionary<int, ObjectPool> enemyObjectPools = new Dictionary<int, ObjectPool>();

    private void Awake()
    {
        for(int i = 0; i < weightedEnemies.Count; i++)
        {
            enemyObjectPools.Add(i, ObjectPool.CreateInstance(weightedEnemies[i].enemy.prefab, numOfEnemiesToSpawn));
        }

        weights = new float[weightedEnemies.Count];
    }

    private void Start()
    {
        triangulation = NavMesh.CalculateTriangulation();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        ResetSpawnWeights();

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
            else if(enemySpawnMethod == SpawnMethod.WeightedRandom)
            {
                SpawnWeightedRandomEnemy();
            }

            spawnedEnemies++;

            yield return wait;
        }
    }

    private void ResetSpawnWeights()
    {
        float totalWeight = 0;

        for(int i = 0; i < weightedEnemies.Count; i++)
        {
            weights[i] = weightedEnemies[i].GetWeight();
            totalWeight += weights[i];
        }

        for(int i = 0; i < weights.Length; i++)
        {
            weights[i] = weights[i] / totalWeight;
        }
    }

    private void SpawnRoundRobinEnemy(int spawnedEnemies)
    {
        int spawnIndex = spawnedEnemies % weightedEnemies.Count;

        DoSpawnEnemy(spawnIndex, ChooseRandomPositionOnNavMesh());
    }

    private void SpawnRandomEnemy()
    {
        DoSpawnEnemy(Random.Range(0, weightedEnemies.Count), ChooseRandomPositionOnNavMesh());
    }

    private void SpawnWeightedRandomEnemy()
    {
        float value = Random.value;

        for(int i = 0; i < weights.Length; i++)
        {
            if(value < weights[i])
            {
                DoSpawnEnemy(i, ChooseRandomPositionOnNavMesh());
                return;
            }

            value -= weights[i];
        }

        Debug.LogError("Invalid configuration! Could not spawn a weighted random enemy. Did you forget to call ResetSpawnWeights()?");
    }

    private Vector3 ChooseRandomPositionOnNavMesh()
    {
        int vertexIndex = Random.Range(0, triangulation.vertices.Length);
        return triangulation.vertices[vertexIndex];
    }

    public void DoSpawnEnemy(int spawnIndex, Vector3 spawnPosition)
    {
        PoolableObject poolableObject = enemyObjectPools[spawnIndex].GetObject();

        if(poolableObject != null)
        {
            EnemyBase enemy = poolableObject.GetComponent<EnemyBase>();
            weightedEnemies[spawnIndex].enemy.SetUpEnemy(enemy);

           

            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, 2f, -1))
            {
                enemy.agent.Warp(hit.position);
                enemy.movement.target = player;
                enemy.movement.triangulation = triangulation;
                enemy.agent.enabled = true;
                enemy.movement.Spawn();
            }
            else
            {
                Debug.LogError($"Unable to place NavMeshAgent on NavMesh. Tried to use {spawnPosition}.");
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
        Random,
        WeightedRandom
    }
}

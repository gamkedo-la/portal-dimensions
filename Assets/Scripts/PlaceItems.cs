using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlaceItems : MonoBehaviour
{
    private Collider[] spawnColliders;
    [SerializeField] public int numOfItemsToSpawn;
    public List<ItemCollection> items = new List<ItemCollection>();
    private Dictionary<int, ObjectPool> itemObjectPools = new Dictionary<int, ObjectPool>();
    private Bounds spawnBounds;

    private void Awake()
    {
        if (transform.childCount > 0 && gameObject.GetComponentInChildren<Collider>() != null)
        {
            spawnColliders = gameObject.GetComponentsInChildren<Collider>();
        }

        if (spawnColliders == null)
        {
            spawnColliders = new Collider[1];
            spawnColliders[0] = GetComponent<Collider>();
        }

        for (int i = 0; i < items.Count; i++)
        {
            itemObjectPools.Add(i, ObjectPool.CreateInstance(items[i].item, numOfItemsToSpawn));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        int spawnCount = 0;
        while(spawnCount < numOfItemsToSpawn)
        {
            int spawnIndex = spawnCount % items.Count;
            DoSpawnItem(spawnIndex, GetRandomPositionInBounds());
            spawnCount++;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GetRandomPositionInBounds()
    {
        Vector3 randomBounds;
        int i = Random.Range(0, spawnColliders.Count());
        spawnBounds = spawnColliders[i].bounds;
        randomBounds = new Vector3(Random.Range(spawnBounds.min.x, spawnBounds.max.x), spawnBounds.min.y, Random.Range(spawnBounds.min.z, spawnBounds.max.z));
        return randomBounds;
    }

    public void DoSpawnItem(int spawnIndex, Vector3 spawnPosition)
    {
        PoolableObject poolableObject = itemObjectPools[spawnIndex].GetObject();

        if (poolableObject != null)
        {
            Item item = poolableObject.GetComponent<Item>();
               
            item.transform.position = spawnPosition;
        }
        else
        {
            Debug.LogError($"Unable to fetch item of type {spawnIndex} from object pool. Out of objects?");
        }
    }
}

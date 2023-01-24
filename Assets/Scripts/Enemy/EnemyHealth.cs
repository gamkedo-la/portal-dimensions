using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBase
{
    public EnemyScriptableObject enemy;
    public float waitTillDeath;
    public GameObject dropTreats;

    [SerializeField] public int numOfTreatsToSpawn;
    public List<ItemCollection> items = new List<ItemCollection>();
    private Dictionary<int, ObjectPool> itemObjectPools = new Dictionary<int, ObjectPool>();

    private void Awake()
    {
        //hurtSound = enemy.hurtSound;
        //healSound = enemy.healSound;
        //killedSound = enemy.killedSound;

        for (int i = 0; i < items.Count; i++)
        {
            itemObjectPools.Add(i, ObjectPool.CreateInstance(items[i].item, numOfTreatsToSpawn));
        }
    }

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerBark")
        {
            GameObject child = collision.gameObject;
            while(child != null)
            {
                if(child.transform.parent != null && child.transform.parent.GetComponent<PlayerAttacking>())
                {
                    PlayerAttacking player = collision.gameObject.GetComponentInParent<PlayerAttacking>();
                    player.HitEnemy();
                    Destroy(collision.gameObject);
                }
                else
                {
                    child = child.transform.parent.gameObject;
                }
            }
        }
    }

    protected override void Killed(GameObject character)
    {
        base.Killed(character);
        StartCoroutine(KillEnemy());
    }

    private IEnumerator KillEnemy()
    {
        int spawnCount = 0;
        while (spawnCount < numOfTreatsToSpawn)
        {
            int spawnIndex = spawnCount % items.Count;
            DoSpawnItem(spawnIndex);
            spawnCount++;
        }

        yield return new WaitForSeconds(waitTillDeath);
        gameObject.SetActive(false);
    }

    public void DoSpawnItem(int spawnIndex)
    {
        PoolableObject poolableObject = itemObjectPools[spawnIndex].GetObject();

        if (poolableObject != null)
        {
            Item item = poolableObject.GetComponent<Item>();

            item.transform.position = transform.position;
        }
        else
        {
            Debug.LogError($"Unable to fetch item of type {spawnIndex} from object pool. Out of objects?");
        }
    }
}

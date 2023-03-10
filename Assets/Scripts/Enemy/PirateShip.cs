using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateShip : MonoBehaviour
{
    public EnemyFieldOfView enemyFieldOfView;

    //shooting cannons
    private float lastShootTime;
    [SerializeField] float shootDelay = 1f;
    private ObjectPool bulletPool;
    public Bullet bulletPrefab;
    private Bullet bullet;
    public Vector3 bulletSpawnOffset = new Vector3(0, 1, 0);
    [SerializeField] private GameObject bulletSpawnL;
    [SerializeField] private GameObject bulletSpawnR;
    [SerializeField] private int damage = 5;
    private bool spawnLeft = false;
    private Coroutine AttackCoroutine;
    private void OnEnable()
    {
        CreateBulletPool();
        enemyFieldOfView.OnGainSight += HandleGainSight;
        enemyFieldOfView.OnLoseSight += HandleLoseSight;
    }

    private void HandleGainSight(Player player)
    {
        Debug.Log("Gain sight");
        if(AttackCoroutine == null)
        {
            AttackCoroutine = StartCoroutine(Attack(player));
        }
    }

    private void HandleLoseSight(Player player)
    {
        Debug.Log("Lose sight");
        //stop firing cannon
        if (AttackCoroutine != null)
        {
            StopCoroutine(AttackCoroutine);
            AttackCoroutine = null;
        }

    }

    private void OnDisable()
    {
        enemyFieldOfView.OnGainSight -= HandleGainSight;
        enemyFieldOfView.OnLoseSight -= HandleLoseSight;
    }

    private IEnumerator Attack(Player player)
    {
        Debug.Log("Attack!");
        WaitForSeconds Wait = new WaitForSeconds(shootDelay);

        yield return Wait;

        Shoot(player);

        AttackCoroutine = null;
    }

    public void CreateBulletPool()
    {
        if (bulletPool == null)
        {
            bulletPool = ObjectPool.CreateInstance(bulletPrefab, Mathf.CeilToInt((1 / shootDelay) * bulletPrefab.autoDestroyTime));
        }
    }

    private void Shoot(Player player)
    {
        GameObject spawnPoint;
        if (spawnLeft)
        {
            spawnPoint = bulletSpawnL;
        }
        else
        {
            spawnPoint = bulletSpawnR;
        }

        PoolableObject poolableObject = bulletPool.GetObject();
        if (poolableObject != null)
        {
            Debug.Log("Shoot cannon at " + player.transform);
            bullet = poolableObject.GetComponent<Bullet>();

            bullet.transform.position = spawnPoint.transform.position + bulletSpawnOffset;
            bullet.transform.rotation = spawnPoint.transform.rotation;

            if (player != null)
            {
                bullet.Spawn(spawnPoint.transform.forward, damage, player.transform);
            }
            else
            {
                bullet.Spawn(spawnPoint.transform.forward, damage, null);
            }

            spawnLeft = !spawnLeft;
        }
    }
}

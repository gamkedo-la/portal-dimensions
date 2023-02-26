using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PlayerAttacking : MonoBehaviour
{
    [SerializeField] public float range;
    [SerializeField] ParticleSystem barkParticles;
    [SerializeField] float shootDelay = 0.5f;
    [SerializeField] LayerMask mask;
    [HideInInspector] public bool isAttacking;
    [SerializeField] public string attackSound;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] public float hitAngle = 30f;
    [SerializeField] public AttackRadius attackRadius;
    [SerializeField] public GameObject barkPoint;
    [SerializeField] public GameObject barkBall;

    private IDamageable enemy;

    //shooting rockets
    private ObjectPool bulletPool;
    public Bullet bulletPrefab;
    private Bullet bullet;
    public Vector3 bulletSpawnOffset = new Vector3(0, 1, 0);
    [SerializeField] private GameObject bulletSpawnL;
    [SerializeField] private GameObject bulletSpawnR;
    [SerializeField] private int damage = 5;
    private bool spawnLeft = false;
    

    private bool isRunning;
    private bool hitEnemy = false;
    private bool rocketTime = false;
    private float lastShootTime;

    private AudioManager audioManager;

    PlayerStateMachine playerState;

    private void OnEnable()
    {
        Rocket.RocketCollected += ShootRockets;
        attackRadius.AttackEnemy += SetEnemy;
        playerState = GetComponent<PlayerStateMachine>();
        isRunning = false;
    }

    private void OnDisable()
    {
        Rocket.RocketCollected -= ShootRockets;
        attackRadius.AttackEnemy -= SetEnemy;
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }
        barkPoint.transform.localScale = new Vector3(barkPoint.transform.localScale.x, barkPoint.transform.localScale.y, range);
        //isAttacking = true;
        //OnAttack(damageable);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Running();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopRunning();
        }
        if(Input.GetKeyDown(KeyCode.F) && !rocketTime)
        {
            Bark();
        }
        else if (Input.GetKeyDown(KeyCode.F) && rocketTime)
        {
            Shoot();
        }
    }

    public void CreateBulletPool()
    {
        if (bulletPool == null)
        {
            bulletPool = ObjectPool.CreateInstance(bulletPrefab, Mathf.CeilToInt((1 / shootDelay) * bulletPrefab.autoDestroyTime));
        }
    }

    private void SetEnemy(IDamageable enemy)
    {
        this.enemy = enemy;

        if(isRunning)
        {
            OnChargeHit();
        }
    }

    private void Running()
    {
        Debug.Log("[PlayerAttacking] Running()");
        isRunning = true;
        playerState.SetIsRunning(true);
    }

    private void StopRunning()
    {
        isRunning = false;
        playerState.SetIsRunning(false);
    }

    private void OnChargeHit()
    {
        Debug.Log("collided");
        if(enemy.GetTransform().gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = enemy.GetTransform().transform.GetComponent<EnemyHealth>();
            HealthBase player = gameObject.GetComponent<HealthBase>();
            enemyHealth.TakeDamage(player.attacker.damage);
        }
    }

    private void Shoot()
    {
        
        if (lastShootTime + shootDelay < Time.time)
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
                Debug.Log("Shoot");
                bullet = poolableObject.GetComponent<Bullet>();

                bullet.transform.position = transform.position + bulletSpawnOffset;
                bullet.transform.rotation = spawnPoint.transform.rotation;
                bullet.gameObject.layer = 7;

                if(enemy != null)
                {
                    bullet.Spawn(spawnPoint.transform.forward, damage, enemy.GetTransform());
                }
                else
                {
                    bullet.Spawn(spawnPoint.transform.forward, damage, null);
                }

            }

            lastShootTime = Time.time;
        }
    }

    private void Bark()
    {
        if(lastShootTime + shootDelay < Time.time)
        {
            audioManager.Play(attackSound);
            barkParticles.Play(true);
            ShootBark();

            Collider[] rangeChecks = Physics.OverlapSphere(spawnPoint.position, range, mask);
            //Debug.Log("First: " + rangeChecks[0].name);
            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - spawnPoint.position).normalized;
                Debug.Log("Second: " + rangeChecks[0].name);
                Debug.Log(Vector3.Angle(spawnPoint.forward, directionToTarget));

                if (Vector3.Angle(spawnPoint.forward, directionToTarget) < hitAngle / 2 || hitEnemy)
                {
                    float distanceToTarget = Vector3.Distance(spawnPoint.position, target.position);
                    Debug.Log("Direction to target: " + directionToTarget);
                    Debug.Log("Dist to target: " + distanceToTarget);
                    Debug.Log("Target layer: " + target.gameObject.layer);
                    if (Physics.Raycast(spawnPoint.position, directionToTarget, range, mask))
                    {
                        Debug.Log("Here");
                        EnemyHealth enemyHealth = target.transform.GetComponent<EnemyHealth>();
                        HealthBase player = gameObject.GetComponent<HealthBase>();
                        enemyHealth.TakeDamage(player.attacker.damage);
                    }
                }
            }
            HitEnemy();
            lastShootTime = Time.time;                    
        }
    }

    public void ShootBark()
    {
        GameObject ball = Instantiate(barkBall, transform.position, transform.rotation);
        ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (gameObject.transform.forward.x, gameObject.transform.forward.y, gameObject.transform.forward.z + 700f));
        Destroy(ball, 5);
    }

    private void ShootRockets()
    {
        rocketTime = true;
        CreateBulletPool();
    }

    public void HitEnemy()
    {
        hitEnemy = !hitEnemy;
    }

    private void OnDrawGizmos()
    {
        Vector3 viewAngle3 = DirectionFromAngle(spawnPoint.eulerAngles.y, -hitAngle / 2);
        Vector3 viewAngle4 = DirectionFromAngle(spawnPoint.eulerAngles.y, hitAngle / 2);

        //Handles.DrawLine(spawnPoint.position, spawnPoint.position + viewAngle3 * range);
        //Handles.DrawLine(spawnPoint.position, spawnPoint.position + viewAngle4 * range);
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }
}

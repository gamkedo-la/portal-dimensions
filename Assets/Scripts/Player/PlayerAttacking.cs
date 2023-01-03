using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    IDamageable damageable;
    [SerializeField] public float range;
    [SerializeField] ParticleSystem barkParticles;
    [SerializeField] float shootDelay = 0.5f;
    [SerializeField] LayerMask mask;
    [HideInInspector] public bool isAttacking;
    [SerializeField] public string attackSound;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] public float hitAngle = 30f; 

    private float lastShootTime;

    private AudioManager audioManager;

    PlayerStateMachine playerState;

    private void OnEnable()
    {
        playerState = GetComponent<PlayerStateMachine>();    
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }

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
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Bark();
        }
    }

    private void Running()
    {
        playerState.SetIsRunning(true);
    }

    private void StopRunning()
    {
        playerState.SetIsRunning(false);
    }

    private void Bark()
    {
        if(lastShootTime + shootDelay < Time.time)
        {
            audioManager.Play(attackSound);
            barkParticles.Play(true);

            Collider[] rangeChecks = Physics.OverlapSphere(spawnPoint.position, range, mask);
            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - spawnPoint.position).normalized;

                if (Vector3.Angle(spawnPoint.forward, directionToTarget) < hitAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(spawnPoint.position, target.position);
                    Debug.Log("Direction to target: " + directionToTarget);
                    Debug.Log("Dist to target: " + distanceToTarget);
                    Debug.Log(target.gameObject.layer);
                    if (Physics.Raycast(spawnPoint.position, directionToTarget, range, mask))
                    {
                        Debug.Log("Here");
                        EnemyHealth enemyHealth = target.transform.GetComponent<EnemyHealth>();
                        HealthBase player = gameObject.GetComponent<HealthBase>();
                        enemyHealth.TakeDamage(player.attacker.damage);
                    }
                }
            }
            
            lastShootTime = Time.time;


                    /*
                    Vector3 direction = transform.forward;

                    if (Physics.Raycast(spawnPoint.position, direction, out RaycastHit hit, range, mask))
                    {
                        if(hit.transform.gameObject.tag == "Enemy")
                        {
                            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                            HealthBase player = gameObject.GetComponent<HealthBase>();
                            enemyHealth.TakeDamage(player.attacker.damage);
                        }
                    }
                    */
                    
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 viewAngle3 = DirectionFromAngle(spawnPoint.eulerAngles.y, -hitAngle / 2);
        Vector3 viewAngle4 = DirectionFromAngle(spawnPoint.eulerAngles.y, hitAngle / 2);

        Handles.DrawLine(spawnPoint.position, spawnPoint.position + viewAngle3 * range);
        Handles.DrawLine(spawnPoint.position, spawnPoint.position + viewAngle4 * range);
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

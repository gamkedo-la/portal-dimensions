using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public abstract class EnemyBase : PoolableObject, IDamageable
{

    [SerializeField] protected float moveSpeed = 5f;
    protected Rigidbody rb;
    protected Transform target;
    protected Vector3 moveDirection;
    public HealthBase health;
    public EnemyScriptableObject enemy;
    [SerializeField] protected int maxHealth;
    public EnemyMovement movement;
    public NavMeshAgent agent;
    public AttackRadius attackRadius;
    public Coroutine LookCoroutine;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        health = GetComponent<HealthBase>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //health = new HealthBase(maxHealth, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            rb.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = direction;
        }
        */
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            target = other.gameObject.transform;
    }
    

    private void FixedUpdate()
    {
        if(target)
        {
            rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z) * moveSpeed;
        }
    }
    */

    private void OnEnable()
    {
        //HealthBase.OnKilled += Killed;
        attackRadius.OnAttack += OnAttack;
        SetUpAgentFromConfiguration();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        //HealthBase.OnKilled -= Killed;
        attackRadius.OnAttack -= OnAttack;
        agent.enabled = false;
    }

    private void OnAttack(IDamageable target)
    {
        //place animation here
        Debug.Log("Attacking!");
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt(target.GetTransform()));
    }

    private IEnumerator LookAt(Transform target)
    {
        //Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        Vector3 lookAtPos = target.position;

        float time = 0;

        while (time < 1)
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            lookAtPos.y = transform.position.y;
            transform.LookAt(lookAtPos);

            time += Time.deltaTime * 2;
            yield return null;
        }

        //transform.rotation = lookRotation;
    }


    public virtual void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " is taking damage");
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public virtual void SetUpAgentFromConfiguration()
    {
        agent.acceleration = enemy.acceleration;
        agent.angularSpeed = enemy.angularSpeed;
        agent.areaMask = enemy.areaMask;
        agent.avoidancePriority = enemy.avoidancePriority;
        agent.baseOffset = enemy.baseOffset;
        agent.height = enemy.height;
        agent.obstacleAvoidanceType = enemy.obstacleAvoidanceType;
        agent.radius = enemy.radius;
        agent.speed = enemy.speed;
        agent.stoppingDistance = enemy.stoppingDistance;

        movement.updateSpeed = enemy.aiUpdateInterval;

        //health = new HealthBase(enemy.health, gameObject);
        health.healthMax = enemy.health;

        attackRadius.sphereCollider.radius = enemy.attackRadius;
        attackRadius.attackDelay = enemy.attackDelay;
        attackRadius.damage = enemy.damage;
    }
}

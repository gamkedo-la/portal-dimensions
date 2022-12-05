using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public abstract class EnemyBase : PoolableObject
{
    [SerializeField] protected float moveSpeed = 5f;
    protected Rigidbody rb;
    protected Transform target;
    protected Vector3 moveDirection;
    public HealthBase health;
    [SerializeField] protected int maxHealth;
    public EnemyMovement movement;
    public NavMeshAgent agent;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = new HealthBase(maxHealth, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            rb.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = direction;
        }
    }

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

    private void OnEnable()
    {
        HealthBase.OnKilled += Killed;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        HealthBase.OnKilled -= Killed;
        agent.enabled = false;
    }

    protected virtual void Killed(GameObject character)
    {
        Debug.Log(gameObject.name + "Killed");       
    }
}

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
    public AttackScriptableObject enemy;
    [SerializeField] protected int maxHealth;
    public EnemyMovement movement;
    public NavMeshAgent agent;
    public AttackRadius attackRadius;
    public Coroutine LookCoroutine;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    protected AudioManager audioManager;
    [HideInInspector] public string attackSound;
    [HideInInspector] public string hurtSound;
    [HideInInspector] public string killedSound;
    [HideInInspector] public string healSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        health = GetComponent<HealthBase>();
    }

    private void OnEnable()
    {
        //HealthBase.OnKilled += Killed;
        attackRadius.OnAttack += OnAttack;
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }
        //attackSound = enemy.attackSound;
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
        //Debug.Log("Attacking!");
        audioManager.Play(attackSound);
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
        //Debug.Log(gameObject.name + " is taking damage");
    }

    public Transform GetTransform()
    {
        return transform;
    }
}

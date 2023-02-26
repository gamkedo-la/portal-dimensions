using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : PoolableObject
{
    public float autoDestroyTime = 5f;
    public float moveSpeed;
    public int damage = 5;
    public Rigidbody rb;
    protected Transform target;

    protected const string DISABLE_METHOD_NAME = "Disable";

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void OnEnable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME, autoDestroyTime);
    }

    public virtual void Spawn(Vector3 forward, int damage, Transform target)
    {
        this.damage = damage;
        this.target = target;
        rb.AddForce(forward * moveSpeed, ForceMode.VelocityChange);
        //transform.position = forward * moveSpeed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;
        Debug.Log(other.gameObject.name);

        if (other.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.TakeDamage(damage);
        }

        if(!other.isTrigger)
            Disable();

    }

    protected void Disable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
    public SphereCollider sphereCollider;
    protected List<IDamageable> Damageables = new List<IDamageable>();
    public int damage = 10;
    public float attackDelay = 0.5f;
    public delegate void AttackEvent(IDamageable Target);
    public AttackEvent OnAttack;
    protected Coroutine AttackCoroutine;

    protected virtual void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by " + other.name);
        IDamageable damageable = other.GetComponent<IDamageable>();
        if(damageable != null)
        {
            Debug.Log("AttackRadius OnTriggerEnter()");
            Damageables.Add(damageable);

            if(AttackCoroutine == null)
            {
                Debug.Log("AttackCorourine == null");
                AttackCoroutine = StartCoroutine(Attack());
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        Debug.Log("AttackRadius OnTriggerExit()");
        if (damageable != null)
        {
            Damageables.Remove(damageable);
            if(Damageables.Count == 0)
            {
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
            }
        }
    }

    protected virtual IEnumerator Attack()
    {
        Debug.Log("AttackRadius Attack()");
        WaitForSeconds Wait = new WaitForSeconds(attackDelay);

        yield return Wait;

        IDamageable closestDamageable = null;
        float closestDistance = float.MaxValue;

        while(Damageables.Count > 0)
        {
            for(int i =0; i < Damageables.Count; i++)
            {
                Transform damageableTransform = Damageables[i].GetTransform();
                float distance = Vector3.Distance(transform.position, damageableTransform.position);

                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDamageable = Damageables[i];
                }
            }

            if(closestDamageable != null)
            {
                OnAttack?.Invoke(closestDamageable);
                closestDamageable.TakeDamage(damage);
            }

            closestDamageable = null;
            closestDistance = float.MaxValue;

            yield return Wait;

            Damageables.RemoveAll(DisabledDamageables);
        }

        AttackCoroutine = null;
    }

    protected bool DisabledDamageables(IDamageable damageable)
    {
        return damageable != null && !damageable.GetTransform().gameObject.activeSelf;
    }
}

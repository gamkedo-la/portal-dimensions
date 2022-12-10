/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttackRadius : AttackRadius
{
    public NavMeshAgent agent;
    public Bullet bulletPrefab;
    public Vector3 bulletSpawnOffset = new Vector3(0, 1, 0);
    public LayerMask mask;
    public ObjectPool bulletPool;
    [SerializeField] private float spherecastRadius = 0.1f;
    private RaycastHit hit;
    private IDamageable targetDamageable;
    private Bullet bullet;

    public void CreateBulletPool()
    {
        if(bulletPool == null)
        {
            bulletPool = ObjectPool.CreateInstance(bulletPrefab, Mathf.CeilToInt((1 / attackDelay) * bulletPrefab.autoDestroyTime));
        }
    }

    protected override IEnumerator Attack()
    {
        Debug.Log("RangedAttackRadius Attack()");
        WaitForSeconds wait = new WaitForSeconds(attackDelay);
        Debug.Log("Before yield return wait" + Damageables.Count);
        yield return wait;
        Debug.Log("After yield return wait" + Damageables.Count);


        while (Damageables.Count > 0)
        {
            Debug.Log(Damageables.Count);
            for(int i = 0; i < Damageables.Count; i++)
            {
                if (HasLineOfSight(Damageables[i].GetTransform()))
                {
                    targetDamageable = Damageables[i];
                    Debug.Log("Before OnAttack()");
                    OnAttack?.Invoke(Damageables[i]);
                    Debug.Log("After OnAttack()");
                    agent.enabled = false;
                    break;
                }
            }

            if(targetDamageable != null)
            {
                PoolableObject poolableObject = bulletPool.GetObject();
                if(poolableObject != null)
                {
                    bullet = poolableObject.GetComponent<Bullet>();

                    bullet.transform.position = transform.position + bulletSpawnOffset;
                    bullet.transform.rotation = agent.transform.rotation;
                    bullet.Spawn(agent.transform.forward, damage, targetDamageable.GetTransform());
                }
                else
                {
                    agent.enabled = true;
                }

                yield return wait;

                if(targetDamageable == null || HasLineOfSight(targetDamageable.GetTransform()))
                {
                    agent.enabled = true;
                }

                Damageables.RemoveAll(DisabledDamageables);
            }

            agent.enabled = true;
            AttackCoroutine = null;
        }
    }

    private bool HasLineOfSight(Transform target)
    {
        Debug.Log("Has line of sight");
        if (Physics.SphereCast(transform.position + bulletSpawnOffset, spherecastRadius, ((target.position + bulletSpawnOffset) - (transform.position + bulletSpawnOffset)).normalized, out hit, sphereCollider.radius, mask))
        {
            IDamageable damageable;

            if(hit.collider.TryGetComponent<IDamageable>(out damageable))
            {
                return damageable.GetTransform() == target;
            }
            return hit.collider.GetComponent<IDamageable>() != null;
        }

        return false;
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if(AttackCoroutine == null)
        {
            agent.enabled = true;
        }
    }
}
*/
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttackRadius : AttackRadius
{
    public NavMeshAgent Agent;
    public Bullet BulletPrefab;
    public Vector3 BulletSpawnOffset = new Vector3(0, 1, 0);
    public LayerMask Mask;
    private ObjectPool BulletPool;
    [SerializeField]
    private float SpherecastRadius = 0.1f;
    private RaycastHit Hit;
    private IDamageable targetDamageable;
    private Bullet bullet;

    public void CreateBulletPool()
    {
        if (BulletPool == null)
        {
            BulletPool = ObjectPool.CreateInstance(BulletPrefab, Mathf.CeilToInt((1 / attackDelay) * BulletPrefab.autoDestroyTime));
        }
    }

    protected override IEnumerator Attack()
    {
        Debug.Log("RangedAttackRadius Attack()");
        WaitForSeconds Wait = new WaitForSeconds(attackDelay);
        Debug.Log("Before yield return wait" + Damageables.Count);
        yield return Wait;
        Debug.Log("After yield return wait" + Damageables.Count);

        while (Damageables.Count > 0)
        {
            for (int i = 0; i < Damageables.Count; i++)
            {
                if (HasLineOfSightTo(Damageables[i].GetTransform()))
                {
                    targetDamageable = Damageables[i];
                    OnAttack?.Invoke(Damageables[i]);
                    Agent.enabled = false;
                    break;
                }
            }

            if (targetDamageable != null)
            {
                PoolableObject poolableObject = BulletPool.GetObject();
                if (poolableObject != null)
                {
                    bullet = poolableObject.GetComponent<Bullet>();

                    bullet.transform.position = transform.position + BulletSpawnOffset;
                    bullet.transform.rotation = Agent.transform.rotation;

                    bullet.Spawn(Agent.transform.forward, damage, targetDamageable.GetTransform());
                }
            }
            else
            {
                Agent.enabled = true; // no target in line of sight, keep trying to get closer
            }

            yield return Wait;

            if (targetDamageable == null || !HasLineOfSightTo(targetDamageable.GetTransform()))
            {
                Agent.enabled = true;
            }

            Damageables.RemoveAll(DisabledDamageables);
        }

        Agent.enabled = true;
        AttackCoroutine = null;
    }

    private bool HasLineOfSightTo(Transform Target)
    {
        Debug.Log("Has line of sight");
        if (Physics.SphereCast(transform.position + BulletSpawnOffset, SpherecastRadius, ((Target.position + BulletSpawnOffset) - (transform.position + BulletSpawnOffset)).normalized, out Hit, sphereCollider.radius, Mask))
        {
            IDamageable damageable;
            if (Hit.collider.TryGetComponent<IDamageable>(out damageable))
            {
                return damageable.GetTransform() == Target;
            }
        }

        return false;
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (AttackCoroutine == null)
        {
            Agent.enabled = true;
        }
    }
}

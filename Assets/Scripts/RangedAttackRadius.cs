using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttackRadius : AttackRadius
{
    public NavMeshAgent agent;
    public Bullet bulletPrefab;
    public Vector3 bulletSpawnOffset = new Vector3(0, 1, 0);
    public LayerMask mask;
    private ObjectPool bulletPool;
    [SerializeField]
    private float spherecastRadius = 0.1f;
    private RaycastHit hit;
    private IDamageable targetDamageable;
    private Bullet bullet;

    public void CreateBulletPool()
    {
        if (bulletPool == null)
        {
            bulletPool = ObjectPool.CreateInstance(bulletPrefab, Mathf.CeilToInt((1 / attackDelay) * bulletPrefab.autoDestroyTime));
        }
    }

    protected override IEnumerator Attack()
    {
        Debug.Log("RangedAttackRadius Attack()");
        WaitForSeconds Wait = new WaitForSeconds(attackDelay);
        yield return Wait;

        while (Damageables.Count > 0)
        {
            for (int i = 0; i < Damageables.Count; i++)
            {
                if (HasLineOfSightTo(Damageables[i].GetTransform()))
                {
                    targetDamageable = Damageables[i];
                    OnAttack?.Invoke(Damageables[i]);
                    agent.enabled = false;
                    //Debug.Log("target damageable: " + targetDamageable);
                    break;
                }
            }

            if (targetDamageable != null)
            {
                //Debug.Log("target damageable: " + targetDamageable);
                PoolableObject poolableObject = bulletPool.GetObject();
                if (poolableObject != null)
                {
                    bullet = poolableObject.GetComponent<Bullet>();

                    bullet.transform.position = transform.position + bulletSpawnOffset;
                    bullet.transform.rotation = agent.transform.rotation;

                    bullet.Spawn(agent.transform.forward, damage, targetDamageable.GetTransform());
                }
            }
            else
            {
                agent.enabled = true; // no target in line of sight, keep trying to get closer
            }

            yield return Wait;

            if (targetDamageable == null || !HasLineOfSightTo(targetDamageable.GetTransform()))
            {
                agent.enabled = true;
            }

            Damageables.RemoveAll(DisabledDamageables);
        }

        agent.enabled = true;
        AttackCoroutine = null;
    }

    private bool HasLineOfSightTo(Transform Target)
    {
        //Debug.Log(transform.position + bulletSpawnOffset);
        //Debug.Log(((Target.position + bulletSpawnOffset) - (transform.position + bulletSpawnOffset)).normalized);
        //Debug.Log(mask.ToString());
        if (Physics.SphereCast(transform.position + bulletSpawnOffset, spherecastRadius, ((Target.position + bulletSpawnOffset) - (transform.position + bulletSpawnOffset)).normalized, out hit, sphereCollider.radius, mask))
        {
            Debug.Log("Has line of sight");
            IDamageable damageable;
            if (hit.collider.TryGetComponent<IDamageable>(out damageable))
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
            agent.enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    public Transform player;
    [SerializeField] public float radius = 5;
    [Range(0, 360)]
    [SerializeField] public float viewAngle;
    [SerializeField] public LayerMask targetMask;
    [SerializeField] public LayerMask obstacleMask;

    public bool canSeePlayer;
    Ray ray;
    EnemyMovement enemyMovement;

    public delegate void GainSightEvent(Player player);
    public GainSightEvent OnGainSight;
    public delegate void LoseSightEvent(Player player);
    public LoseSightEvent OnLoseSight;

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    //Debug.Log(target.transform.gameObject.name, target.gameObject);
                    canSeePlayer = true;
                    player = target;
                    OnGainSight?.Invoke(target.gameObject.GetComponent<Player>());
                }
                else
                {
                    canSeePlayer = false;
                    OnLoseSight?.Invoke(target.GetComponent<Player>());
                }
            }
            else
            {
                canSeePlayer = false;
                OnLoseSight?.Invoke(target.GetComponent<Player>());
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
            OnLoseSight?.Invoke(player.gameObject.GetComponent<Player>());
        }
    }

    public bool CanSeePlayer()
    {
        return false;
    }
}

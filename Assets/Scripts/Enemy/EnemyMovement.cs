using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float updateSpeed = 0.1f;

    private NavMeshAgent agent;
    private float distanceFromTarget;

    public EnemyScriptableObject enemy;
    private Coroutine FollowCoroutine;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();  
    }
    // Start is called before the first frame update
    public void StartChasing()
    {
        if(FollowCoroutine == null)
        {
            StartCoroutine(FollowTarget());
        }
        else
        {
            Debug.LogWarning("Called StartChasing on Enemy that is already chasing! This is likely a bug in some calling class!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        while(enabled)
        {
            distanceFromTarget = Vector3.Distance(transform.position, target.position);

            if(distanceFromTarget <= enemy.stoppingDistance)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
            }
            

            yield return wait;
        }
    }
}

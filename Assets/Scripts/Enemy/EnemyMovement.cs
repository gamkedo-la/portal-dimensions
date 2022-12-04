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
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();  
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowTarget());
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
            agent.SetDestination(target.transform.position);

            yield return wait;
        }
    }
}

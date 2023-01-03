using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    //public EnemyLineOfSightChecker sightChecker;
    public EnemyFieldOfView enemyFieldOfView;
    public float updateSpeed = 0.1f;

    private NavMeshAgent agent;
    public NavMeshTriangulation triangulation;
    private float distanceFromTarget;

    public EnemyScriptableObject enemy;
    private Coroutine FollowCoroutine;

    public EnemyState defaultState;
    public EnemyState currentState;

    public EnemyState State 
    { 
        get 
        { 
            return currentState; 
        } 
        set 
        {
            OnStateChange?.Invoke(currentState, value);
            currentState = value; 
        } 
    }

    public delegate void StateChangeEvent(EnemyState oldState, EnemyState newState);
    public StateChangeEvent OnStateChange;
    public float idleLocationRadius = 4f;
    public float idleMoveSpeedMultiplier = 0.5f;
    [SerializeField] private int waypointIndex = 0;

    public Vector3[] waypoints = new Vector3[4];

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        enemyFieldOfView.OnGainSight += HandleGainSight;
        enemyFieldOfView.OnLoseSight += HandleLoseSight;

        OnStateChange += HandleStateChange;        
    }

    private void HandleGainSight(Player player)
    {
        //Debug.Log("Handle Gain Sight");
        //Debug.Log("Current State: " + State);
        EnemyState oldState = State;
        //Debug.Log("Old State: " + oldState);
        State = EnemyState.Chase;
        //Debug.Log("New State: " + State);
        OnStateChange?.Invoke(oldState, State);
    }

    private void HandleLoseSight(Player player)
    {
        //Debug.Log("Handle Lose Sight");
        EnemyState oldState = State;
        State = defaultState;
        OnStateChange?.Invoke(oldState, State);
    }

    public void Spawn()
    {
        //Debug.Log("Spawn");
        for (int i = 0; i < waypoints.Length; i++)
        {
            NavMeshHit hit;
            if(NavMesh.SamplePosition(triangulation.vertices[Random.Range(0, triangulation.vertices.Length)], out hit, 2f, agent.areaMask))
            {
                waypoints[i] = hit.position;
            }
            else
            {
                Debug.LogError($"Unable to find position for navmesh near triangulation vertex!");
            }
        }

        defaultState = (EnemyState)Random.Range(1, 3);
        //Debug.Log(defaultState);
        OnStateChange?.Invoke(EnemyState.Spawn, defaultState);
    }

    private void OnDisable()
    {
        currentState = defaultState;
        //sightChecker.OnGainSight -= HandleGainSight;
        //sightChecker.OnLoseSight -= HandleLoseSight;
        enemyFieldOfView.OnGainSight -= HandleGainSight;
        enemyFieldOfView.OnLoseSight -= HandleLoseSight;
        //OnStateChange -= HandleStateChange;
    }

    private void HandleStateChange(EnemyState oldState, EnemyState newState)
    {
        //Debug.Log("Handle State Change");
        if (oldState != newState)
        {
            if(FollowCoroutine != null)
            {
                StopCoroutine(FollowCoroutine);
            }

            if(oldState == EnemyState.Idle)
            {
                //Debug.Log("Before: " + agent.speed);
                agent.speed /= idleMoveSpeedMultiplier;
                //Debug.Log("After: " + agent.speed);
            }

            switch (newState)
            {
                case EnemyState.Idle:
                    FollowCoroutine = StartCoroutine(DoIdleMotion());
                    break;
                case EnemyState.Patrol:
                    FollowCoroutine = StartCoroutine(DoPatrolMotion());
                    break;
                case EnemyState.Chase:
                    FollowCoroutine = StartCoroutine(FollowTarget());
                    break;
            }

        }
    }

    private IEnumerator DoIdleMotion()
    {
        //Debug.Log("Do idle motion");
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        agent.speed *= idleMoveSpeedMultiplier;

        while(true)
        {
            if(!agent.enabled || !agent.isOnNavMesh)
            {
                yield return wait;
            }
            else if(agent.remainingDistance <= agent.stoppingDistance)
            {
                Vector2 point = Random.insideUnitCircle * idleLocationRadius;
                NavMeshHit hit;

                if(NavMesh.SamplePosition(agent.transform.position + new Vector3(point.x, 0, point.y), out hit, 2f, agent.areaMask))
                {
                    agent.SetDestination(hit.position);
                }
            }
            yield return wait;
        }
    }

    private IEnumerator DoPatrolMotion()
    {
        //Debug.Log("Do patrol motion");
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        yield return new WaitUntil(() => agent.enabled && agent.isOnNavMesh);
        agent.SetDestination(waypoints[waypointIndex]);

        while(true)
        {
            if(agent.isOnNavMesh && agent.enabled && agent.remainingDistance <= agent.stoppingDistance)
            {
                waypointIndex++;

                if(waypointIndex >= waypoints.Length)
                {
                    waypointIndex = 0;
                }

                agent.SetDestination(waypoints[waypointIndex]);
            }
            yield return wait;
        }
    }

    private IEnumerator FollowTarget()
    {
        //Debug.Log("Do follow target motion");
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        while (enabled)
        {
            distanceFromTarget = Vector3.Distance(transform.position, target.position);

            if (distanceFromTarget <= enemy.stoppingDistance)
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

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.DrawWireSphere(waypoints[i], 0.25f);
            if (i + 1 < waypoints.Length)
            {
                Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
            }
            else
            {
                Gizmos.DrawLine(waypoints[i], waypoints[0]);
            }
        }
    }
}

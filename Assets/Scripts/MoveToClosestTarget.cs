using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToClosestTarget : MonoBehaviour
{
    public Transform[] targets;
    public NavMeshAgent agent;

    public void ChooseTarget()
    {
        float closestTargetDistance = float.MaxValue;
        NavMeshPath path = null;
        NavMeshPath shortestPath = null;

        for(int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == null)
            {
                continue;
            }

            path = new NavMeshPath();

            if(NavMesh.CalculatePath(transform.position, targets[i].position, agent.areaMask, path))
            {
                float distance = Vector3.Distance(transform.position, path.corners[0]);

                for(int j = 0; j < path.corners.Length; j++)
                {
                    distance += Vector3.Distance(path.corners[j - 1], path.corners[j]);
                }

                if(distance < closestTargetDistance)
                {
                    closestTargetDistance = distance;
                    shortestPath = path;
                }
            }
        }

        if(shortestPath != null)
        {
            agent.SetPath(shortestPath);
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 300, 50), "Move To Target"))
        {
            ChooseTarget();
        }
    }
}

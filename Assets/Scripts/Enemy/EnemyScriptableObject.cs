using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Enemy Configuration", menuName = "ScriptableObject/Enemy Configuration")]
public class EnemyScriptableObject : ScriptableObject
{
    public EnemyBase prefab;
    public AttackScriptableObject attackConfiguration;

    //Enemy Stats
    public int health = 100;

    //NavMeshAgent Configs
    public float aiUpdateInterval = 0.1f;

    public float acceleration = 8;
    public float angularSpeed = 120;

    public int areaMask = -1; //-1 to mean everything
    public int avoidancePriority = 50;
    public float baseOffset = 0;
    public float height = 2f;
    public ObstacleAvoidanceType obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public float radius = 0.5f;
    public float speed = 3f;
    public float stoppingDistance = 0.5f;

    public void SetUpEnemy(EnemyBase enemy)
    {
        enemy.agent.acceleration = acceleration;
        enemy.agent.angularSpeed = angularSpeed;
        enemy.agent.areaMask = areaMask;
        enemy.agent.avoidancePriority = avoidancePriority;
        enemy.agent.baseOffset = baseOffset;
        enemy.agent.height = height;
        enemy.agent.obstacleAvoidanceType = obstacleAvoidanceType;
        enemy.agent.radius = radius;
        enemy.agent.speed = speed;
        enemy.agent.stoppingDistance = stoppingDistance;

        enemy.movement.updateSpeed = aiUpdateInterval;

        //health = new HealthBase(health, gameObject);
        enemy.health.healthMax = health;

        attackConfiguration.SetUpEnemy(enemy);
    }
}

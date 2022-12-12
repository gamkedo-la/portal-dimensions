using UnityEngine;

[CreateAssetMenu(fileName = "Weighted Spawn Config", menuName = "ScriptableObject/Weighted Spawn Config")]
public class WeightedSpawnScriptableObject : ScriptableObject
{
    public EnemyScriptableObject enemy;
    [Range(0f, 1f)]
    public float minWeight;
    [Range(0f, 1f)]
    public float maxWeight;

    public float GetWeight()
    {
        return Random.Range(minWeight, maxWeight);
    }
}

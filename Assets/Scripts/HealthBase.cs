using System;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    //public static event Action<GameObject> OnKilled;
    public static event Action<GameObject> OnHealthChanged;
    private int health;
    [SerializeField] public int healthMax;
    private GameObject character;

    public HealthBase(int healthMax, GameObject character)
    {
        this.healthMax = healthMax;
        health = healthMax;
        this.character = character;
    }   

    public int GetHealth()
    {
        return health;  
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        OnHealthChanged?.Invoke(character);
        if (health < 0)
            health = 0;
        if(health <= 0)
        {
            Killed(character);
            //OnKilled?.Invoke(character);
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax)
            health = healthMax;
    }

    protected virtual void Killed(GameObject character)
    {
        Debug.Log(gameObject.name + "Killed");
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}

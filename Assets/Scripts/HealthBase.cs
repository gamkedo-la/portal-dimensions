using System;
using UnityEngine;

public class HealthBase : IDamageable
{
    public static event Action<GameObject> OnKilled;
    private int health;
    private int healthMax;
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
        if(health < 0)
            health = 0;
        if(health <= 0)
        {
            OnKilled?.Invoke(character);
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax)
            health = healthMax;
    }

    public Transform GetTransform()
    {
        return this.GetTransform();
    }
}

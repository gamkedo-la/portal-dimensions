using SoundSystem;
using System;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    //public static event Action<GameObject> OnKilled;
    public static event Action<GameObject> OnHealthChanged;
    private int health;
    [SerializeField] public int healthMax;
    private GameObject character;

    protected AudioManager audioManager;
    public string hurtSound;
    public string killedSound;
    public string healSound;

    /*
    public HealthBase(int healthMax, GameObject character)
    {
        this.healthMax = healthMax;
        health = healthMax;
        this.character = character;
    }   
    */
    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }

        health = healthMax;
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
        OnHealthChanged?.Invoke(character);
        audioManager.Play(hurtSound);
        if (health <= 0)
        {
            Killed(character);
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax)
            health = healthMax;
        OnHealthChanged?.Invoke(character);
        //audioManager.Play(healSound);
    }

    protected virtual void Killed(GameObject character)
    {
        Debug.Log(gameObject.name + "Killed");
        //audioManager.Play(killedSound);
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}

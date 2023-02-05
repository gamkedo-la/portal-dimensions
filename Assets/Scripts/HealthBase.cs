using SoundSystem;
using System;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    //public static event Action<GameObject> OnKilled;
    public static event Action<GameObject> OnHealthChanged;
    public float health;
    [SerializeField] public float healthMax;
    private GameObject character;
    public AttackScriptableObject attacker;

    protected AudioManager audioManager;
    [HideInInspector] public string attackSound;
    [HideInInspector] public string hurtSound;
    [HideInInspector] public string killedSound;
    [HideInInspector] public string healSound;
    
    private bool invulnerable = false;

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
        hurtSound = attacker.hurtSound;
        killedSound = attacker.killedSound;
        //healSound = attacker.healSound;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.I))
        {
            invulnerable = !invulnerable;
        }
#endif
    }

    public float GetHealth()
    {
        return health;  
    }

    public virtual void TakeDamage(int damageAmount)
    {
        if (invulnerable) return;
        health -= damageAmount;
        //OnHealthChanged?.Invoke(character);
        if (health < 0)
            health = 0;
        OnHealthChanged?.Invoke(character);
        audioManager.Play(hurtSound);
        if (health <= 0)
        {
            Killed(character);
        }
    }

    public void Heal(float healAmount)
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

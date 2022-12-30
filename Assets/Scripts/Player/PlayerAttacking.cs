using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    IDamageable damageable;
    [SerializeField] public float range;
    [SerializeField] public ParticleSystem barkParticles;
    [SerializeField] public AttackRadius attackRadius;
    [HideInInspector] public bool isAttacking;
    private AudioManager audioManager;
    public string attackSound;


    private void OnEnable()
    {
        attackRadius.OnAttack += OnAttack;
    }

    private void OnDisable()
    {
        attackRadius.OnAttack -= OnAttack;
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }

        //isAttacking = true;
        //OnAttack(damageable);
        
    }
    private void OnAttack(IDamageable target)
    {
        Debug.Log("Attacking" + target);
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            //place animation here
            audioManager.Play(attackSound);
            barkParticles.Play(true);
        }
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }
}

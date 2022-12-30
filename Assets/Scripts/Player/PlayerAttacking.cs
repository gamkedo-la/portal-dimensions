using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    IDamageable damageable;
    [SerializeField] public float range;
    [SerializeField] ParticleSystem barkParticles;
    [HideInInspector] public bool isAttacking;
    private AudioManager audioManager;
    public string attackSound;
    PlayerStateMachine playerState;

    private void OnEnable()
    {
        playerState = GetComponent<PlayerStateMachine>();    
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Running();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopRunning();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Bark();
        }
    }

    private void Running()
    {
        playerState.SetIsRunning(true);
    }

    private void StopRunning()
    {
        playerState.SetIsRunning(false);
    }

    private void Bark()
    {
        //place animation here
        audioManager.Play(attackSound);
        barkParticles.Play(true);
    }


    public bool GetIsAttacking()
    {
        return isAttacking;
    }
}

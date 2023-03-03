using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : HealthBase
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private bool invuln = false;
    [SerializeField]private GameObject debugTeleportObject;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TakeDamage(5);
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.I))
        {
            debugInvulnerable = !debugInvulnerable;
        }
        if (Input.GetKeyDown(KeyCode.T) && debugTeleportObject)
        {
            GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = debugTeleportObject.transform.position;
            GetComponent<CharacterController>().enabled = true;
        }
#endif
    }

    public void SetInvuln()
    {
        invuln = !invuln;
    }
    
    public override void TakeDamage(int damageAmount)
    {
        if (!invuln)
            base.TakeDamage(damageAmount);
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
    }
    private void UpdateVignette()
    {
        //Vignette
    }

    protected override void Killed(GameObject character)
    {
        base.Killed(character);
    }
}

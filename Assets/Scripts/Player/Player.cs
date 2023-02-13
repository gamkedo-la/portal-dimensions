using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : HealthBase
{
    private bool invuln = false;
    [SerializeField]private GameObject debugTeleportObject;

    private void Update()
    {
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

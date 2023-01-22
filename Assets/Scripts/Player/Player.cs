using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : HealthBase
{
    private bool invuln = false;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBase
{
    public EnemyHealth(int healthMax, GameObject character) : base(healthMax, character)
    {

    }

    protected override void Killed(GameObject character)
    {
        base.Killed(character);
    }
}

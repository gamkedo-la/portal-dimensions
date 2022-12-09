using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBase
{
    public EnemyScriptableObject enemy;
    /*
    public EnemyHealth(int healthMax, GameObject character) : base(healthMax, character)
    {

    }
    */

    private void Awake()
    {
        //enemy = GetComponent<EnemyScriptableObject>();
        //hurtSound = enemy.hurtSound;
        //healSound = enemy.healSound;
        //killedSound = enemy.killedSound;
    }

    protected override void Killed(GameObject character)
    {
        base.Killed(character);
    }
}

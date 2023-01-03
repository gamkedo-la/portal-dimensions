using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBase
{
    public EnemyScriptableObject enemy;


    private void Awake()
    {
        //hurtSound = enemy.hurtSound;
        //healSound = enemy.healSound;
        //killedSound = enemy.killedSound;
    }

    protected override void Killed(GameObject character)
    {
        base.Killed(character);
        gameObject.SetActive(false);
    }
}

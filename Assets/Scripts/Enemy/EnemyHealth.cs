using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBase
{
    public EnemyScriptableObject enemy;
    public float waitTillDeath;


    private void Awake()
    {
        //hurtSound = enemy.hurtSound;
        //healSound = enemy.healSound;
        //killedSound = enemy.killedSound;
    }

    protected override void Killed(GameObject character)
    {
        base.Killed(character);
        StartCoroutine(KillEnemy());
    }

    private IEnumerator KillEnemy()
    {
        yield return new WaitForSeconds(waitTillDeath);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : EnemyBase
{
    protected override void Killed(GameObject character)
    {
        if (character == gameObject)
        {
            base.Killed(character);
        }
        
    }
}

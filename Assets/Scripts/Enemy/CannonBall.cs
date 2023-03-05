using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : HomingBullet
{
    protected override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit!" + other.gameObject.name);
        base.OnTriggerEnter(other);
    }
}

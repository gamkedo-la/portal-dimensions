using System;
using UnityEngine;

public class PowerUpSuperSpeed : Item
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStateMachine>().SetIsRunBoost(true);
            Destroy(transform.gameObject);
        }
            
    }
}

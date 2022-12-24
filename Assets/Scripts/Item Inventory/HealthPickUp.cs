using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float amount = 50;

    private void OnTriggerEnter(Collider other)
    {
        HealthBase health = other.GetComponent<HealthBase>();
        if(health != null)
        {
            health.Heal(amount);
            Destroy(gameObject);
        }
    }
}

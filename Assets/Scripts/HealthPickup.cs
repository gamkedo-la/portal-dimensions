using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthBonus = 15f;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Player>().Heal(healthBonus);
        Destroy(gameObject);
    }
}

using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public static event Action RocketCollected;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            RocketCollected?.Invoke();
        }

    }
}

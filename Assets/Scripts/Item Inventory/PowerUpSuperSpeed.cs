using System;
using UnityEngine;

public class PowerUpSuperSpeed : MonoBehaviour 
{
    [SerializeField] GameObject bolt;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStateMachine>().SetIsRunBoost(true);
            bolt.gameObject.SetActive(false);
        }
            
    }
}

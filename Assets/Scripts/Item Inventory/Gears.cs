using System;
using UnityEngine;

public class Gears : Item
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            collection.amount += worth;
            StatsChanged();
            gameObject.SetActive(false);
        }
            
    }
}

using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Treats : Item
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collection.amount += worth;
            base.OnTriggerEnter(other);
        }

    }
}

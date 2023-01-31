using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPack : Item
{
    public GameObject rocketPack;

    private void OnEnable()
    {
        StoreClerk.PartAdded += AddPart;
    }

    public override void OnDisable()
    {
        StoreClerk.PartAdded -= AddPart;
        base.OnDisable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        rocketPack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPart()
    {
        collection.amount++;
        StatsChanged();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPack : MonoBehaviour
{
    public GameObject rocketPack;
    public Stats stats;

    private void OnEnable()
    {
        StoreClerk.PartAdded += AddPart;
    }

    private void OnDisable()
    {
        StoreClerk.PartAdded -= AddPart;
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
        stats.rocketParts++;
    }
}

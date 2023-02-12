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
        Rocket.RocketCollected += RocketEnabled;
    }

    private void OnDisable()
    {
        StoreClerk.PartAdded -= AddPart;
        Rocket.RocketCollected -= RocketEnabled;
    }

    // Start is called before the first frame update
    void Awake()
    {
        rocketPack.SetActive(false);
    }

    private void Start()
    {
        rocketPack.gameObject.SetActive(false);
    }

    public void RocketEnabled()
    {
        rocketPack.gameObject.SetActive(true);
    }

    public void AddPart()
    {
        stats.rocketParts++;
    }
}

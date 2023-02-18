using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RocketPack : MonoBehaviour
{
    public GameObject rocketPack;
    public Stats stats;
    [SerializeField] TMP_Text instructionsTxt;
    [SerializeField] float timeTextShown;

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
        StartCoroutine(ShowText());
        rocketPack.gameObject.SetActive(true);
    }

    public void AddPart()
    {
        stats.rocketParts++;
    }

    private IEnumerator ShowText()
    {
        WaitForSeconds Wait = new WaitForSeconds(timeTextShown);

        instructionsTxt.text = "Press Q to go up \nPress E to go down\nPress F to fire homing bullet ";
        instructionsTxt.gameObject.SetActive(true);

        yield return Wait;

        instructionsTxt.gameObject.SetActive(false);
    }
}

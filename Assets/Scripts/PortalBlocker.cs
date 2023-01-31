using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortalBlocker : MonoBehaviour
{
    [SerializeField] TMP_Text skyWorldBlockedTxt;
    [SerializeField] float timeTextShown;
    [SerializeField] ItemCollection gears;
    [SerializeField] GameManager gameManager;
    [SerializeField] Stats stats;

    private int gearsNeeded;
    private int rocketNeeded;

    private void Start()
    {
        gearsNeeded = gameManager.GetGearsNeeded();
        rocketNeeded = gameManager.GetRocketPartsNeeded();
    }

    private void OnTriggerEnter(Collider other)
    {
        int totalGears = gears.amount;
        int totalRockets = stats.rocketParts;


        if(totalGears < gearsNeeded && totalRockets < rocketNeeded)
        {
            StartCoroutine(ShowText());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator ShowText()
    {
        WaitForSeconds Wait = new WaitForSeconds(timeTextShown);

        skyWorldBlockedTxt.text = "Cannot enter yet. You must have a total of " + gearsNeeded + " gears and " + rocketNeeded + " rocket parts to build the rocket pack needed here.\nTip: Purchase rocket parts from the store clerk.";
        skyWorldBlockedTxt.gameObject.SetActive(true);

        yield return Wait;

        skyWorldBlockedTxt.gameObject.SetActive(false);
    }
}

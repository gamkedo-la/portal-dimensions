using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class StoreClerk : NPCInteractable
{
    public static event Action Changed;
    public static event Action PartAdded;
    [SerializeField] ItemCollection treats;
    [SerializeField] Stats stats;
    [SerializeField] int costForPart;
    [SerializeField] TMP_Text exchangeText;
    [SerializeField] float timeTextShown;
    [SerializeField] GameManager gameManager;

    public override void Interact()
    {
        base.Interact();
        if(treats.amount >= costForPart && gameManager.GetRocketPartsNeeded() > stats.rocketParts)
        {
            treats.amount -= costForPart;
            costForPart += 5;
            PartAdded?.Invoke();            
            Changed?.Invoke();
        }
        else if(gameManager.GetRocketPartsNeeded() > stats.rocketParts)
        {
            exchangeText.text = costForPart.ToString() + " treats needed to trade";
            StartCoroutine(ShowText());
        }
        else
        {
            exchangeText.text = "You have all the rocket parts you need.";
            StartCoroutine(ShowText());
        }

    }

    private IEnumerator ShowText()
    {
        WaitForSeconds Wait = new WaitForSeconds(timeTextShown);

        //notEnoughText.text = costForPart.ToString() + " treats needed to trade";
        exchangeText.gameObject.SetActive(true);

        yield return Wait;

        exchangeText.gameObject.SetActive(false);
    }   

    public int GetCost()
    {
        return costForPart;
    }
}

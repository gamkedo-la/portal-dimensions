using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private TMP_Text interactText;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private StoreClerk clerk;

    private void OnEnable()
    {
        playerInteract.OnInteractableFound += Show;
        playerInteract.OnInteractableLost += Hide;
        StoreClerk.PartAdded += SetAmountText;
    }

    private void OnDisable()
    {
        playerInteract.OnInteractableFound -= Show;
        playerInteract.OnInteractableLost -= Hide;
        StoreClerk.PartAdded -= SetAmountText;
    }

    private void Start()
    {
        SetAmountText();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        if(playerInteract.GetInteractableObject() != null)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    */

    public void SetAmountText()
    {
        if(clerk != null)
            interactText.text = "Press E to trade " + clerk.GetCost().ToString() + " treats for rocket pack parts";
    }

    private void Show(NPCInteractable target)
    {
        if (interactText == null) 
        {
            return;
        }
        interactText.gameObject.SetActive(true);
    }

    private void Hide(NPCInteractable target)
    {
        if(interactText==null) {
            return;
        }
        interactText.gameObject.SetActive(false);
    }

}

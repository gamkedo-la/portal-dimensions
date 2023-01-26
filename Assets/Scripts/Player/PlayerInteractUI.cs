using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private TMP_Text interactText;
    [SerializeField] private PlayerInteract playerInteract;

    private void OnEnable()
    {
        playerInteract.OnInteractableFound += Show;
        playerInteract.OnInteractableLost += Hide;
    }

    private void OnDisable()
    {
        playerInteract.OnInteractableFound -= Show;
        playerInteract.OnInteractableLost -= Hide;
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

    private void Show(NPCInteractable target)
    {
        interactText.text = "Press E to interact with " + target.name;
        interactText.gameObject.SetActive(true);
    }

    private void Hide(NPCInteractable target)
    {
        interactText.gameObject.SetActive(false);
    }

}

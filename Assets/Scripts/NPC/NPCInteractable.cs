using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = DialogueManager.instance;
        if (dialogueManager == null)
        {
            Debug.LogError("No dialogue manager found in scene");
        }
    }

    public void Interact()
    {
        Debug.Log("Interact");
        dialogueManager.StartDialogue(dialogue);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public delegate void InteractableCloseEvent(NPCInteractable Target);
    public InteractableCloseEvent OnInteractableFound;
    public InteractableCloseEvent OnInteractableLost;
    [SerializeField] public float interactRange = 2f;
    private void FixedUpdate()
    {
        NPCInteractable npc = GetInteractableObject();
        if (Input.GetKeyDown(KeyCode.E) && npc != null)
        {
            npc.Interact();
            /*
            Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
            foreach(Collider collider in colliders)
            {
                if(collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                }
            }
            */
        }
    }

    public NPCInteractable GetInteractableObject()
    {
        bool npcFound = false;
        List<NPCInteractable> interactables = new List<NPCInteractable>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out NPCInteractable npcInteractable))
            {
                npcFound = true;
                interactables.Add(npcInteractable);
                OnInteractableFound?.Invoke(npcInteractable);
            }
        }

        Debug.Log("Colliders count: " + colliders.Count());
        Debug.Log("Interactables count: " + interactables.Count);
        if(!npcFound)
        {
            Debug.Log("Lost");
            OnInteractableLost?.Invoke(null);
        }

        NPCInteractable closestNPCInteractable = null;
        foreach(NPCInteractable npcInteractable in interactables)
        {
            if(closestNPCInteractable == null)
            {
                closestNPCInteractable = npcInteractable;
            }
            else
            {
                if(Vector3.Distance(transform.position, npcInteractable.transform.position) < Vector3.Distance(transform.position, closestNPCInteractable.transform.position))
                {
                    closestNPCInteractable = npcInteractable;
                }
            }
        }
        return closestNPCInteractable;
    }
}

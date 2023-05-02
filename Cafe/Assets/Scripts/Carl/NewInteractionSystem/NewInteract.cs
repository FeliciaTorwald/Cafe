using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewInteract : MonoBehaviour
{
    private List<NewInteractable> interactables = new List<NewInteractable>();
    private NewInteractable heldObjRef;
    private bool holding;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            if (interactables.Count != 0 && !holding)
            {
                FindClosestInteractable().Interact(this);
            }
            else if (interactables.Count != 0 && holding)
            {
                heldObjRef.Interact(this);
            }
        }
    }

    private NewInteractable FindClosestInteractable()
    {
        CleanList();
        if (interactables.Count == 0)
            return null;
        NewInteractable closest = interactables.OrderBy(x => Vector3.Distance(transform.position, x.transform.position))
            .First();
        return closest;
    }

    private void CleanList()
    {
        for (int i = interactables.Count - 1; i >= 0; i--)
        {
            if (interactables[i] == null)
            {
                interactables.RemoveAt(i);
            }
        }        
    }
    
    public void HoldingSomething(NewInteractable newInteractable)
    {
        
    }

    public void NoLongerHoldingSomething()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NewInteractable>() != null)
        {
            interactables.Add(other.GetComponent<NewInteractable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NewInteractable>() != null)
        {
            interactables.Remove(other.GetComponent<NewInteractable>());
        }
    }
}
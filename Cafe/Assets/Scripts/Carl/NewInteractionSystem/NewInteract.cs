using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Carl.NewInteractionSystem;
using UnityEngine;

public class NewInteract : MonoBehaviour
{
    public List<NewAbstractInteractable> interactables = new();
    private NewAbstractInteractable heldObjRef;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            NewAbstractInteractable closest;
            
            if (interactables.Count != 0 && !heldObjRef)
            {
                closest = FindClosestInteractable();
                if (closest)
                    closest.Interact(this);
            }
            else if (interactables.Count != 0 && heldObjRef)
            {
                heldObjRef.Interact(this);
            }
        }
    }

    private NewAbstractInteractable FindClosestInteractable()
    {
        CleanList();
        if (interactables.Count == 0)
            return null;
        NewAbstractInteractable closest = interactables.OrderBy(x => 
                Vector3.Distance(transform.position, x.transform.position)).First();
        return closest;
    }

    private void CleanList()
    {
        for (int i = interactables.Count - 1; i >= 0; i--)
        {
            if (interactables[i] is null)
            {
                interactables.RemoveAt(i);
            }
            else if (interactables[i].newItemType is NewItemType.bobaHandler &&
                     !interactables[i].GetComponent<NewBobaTeaHandler>().guestRef)
            {
                interactables.RemoveAt(i);
            }
        }        
    }
    
    public void HoldingSomething(NewAbstractInteractable newInteractable)
    {
        heldObjRef = newInteractable;
    }

    public void NoLongerHoldingSomething()
    {
        heldObjRef = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NewAbstractInteractable>() != null)
        {
            interactables.Add(other.GetComponent<NewAbstractInteractable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NewAbstractInteractable>() != null)
        {
            interactables.Remove(other.GetComponent<NewAbstractInteractable>());
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Carl.NewInteractionSystem;
using UnityEngine;

public class Interactable_NewFullTea : NewAbstractInteractable
{
    public List<NewBobaTeaHandler> nearbyTables = new();
    private readonly string tableString = "Table";
    [SerializeField] private TeaType teaType;
    
    public override void Interact(NewInteract newInteract)
    {
        playerInteractRef = newInteract;
        
        if (isHeld)
        {
            if (nearbyTables.Count != 0)
            {
                TeaOperations(playerInteractRef);
            }
            else
            {
                Drop(playerInteractRef);
            }
        }
        else
        {
            Hold(playerInteractRef);
        }
    }

    public override void Throw(NewInteract newInteract)
    {
        //Not throwable
    }

    public override void TeaOperations(NewInteract newInteract)
    {
        NewBobaTeaHandler closest = FindClosestTable();

        if (closest.guestRef)
        {
            if (closest.guestRef.stateMachine.currentState is GuestStateID.AtTable)
                closest.TakeOrder(playerInteractRef);
            else if (closest.guestRef.stateMachine.currentState is GuestStateID.Ordered)
            {
                closest.ServeTable(playerInteractRef, gameObject, teaType);
                if (closest.guestRef.stateMachine.currentState is GuestStateID.Served)
                {
                    playerInteractRef.NoLongerHoldingSomething();
                    playerInteractRef.interactables.Remove(this);
                }
            }
        }
        else
        {
            Drop(newInteract);
        }
        
        //Ensure the below function call is included
    }

    public override void WaterOperations(NewInteract newInteract)
    {
        //Not a bucket
    }

    public NewBobaTeaHandler FindClosestTable()
    {
        CleanList();
        if (nearbyTables.Count == 0)
            return null;
        NewBobaTeaHandler closest = nearbyTables.OrderBy(x => Vector3.Distance(transform.position, x.transform.position))
            .First();
        return closest;
    }
    
    private void CleanList()
    {
        for (int i = nearbyTables.Count - 1; i >= 0; i--)
        {
            if (nearbyTables[i] == null)
            {
                nearbyTables.RemoveAt(i);
            }
        }        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tableString))
        {
            nearbyTables.Add(other.GetComponent<NewBobaTeaHandler>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tableString))
        {
            nearbyTables.Remove(other.GetComponent<NewBobaTeaHandler>());
        }
    }
}

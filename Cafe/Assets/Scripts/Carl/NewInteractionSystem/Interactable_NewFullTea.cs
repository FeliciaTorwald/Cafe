using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Carl.NewInteractionSystem;
using UnityEngine;

public class Interactable_NewFullTea : NewAbstractInteractable
{
    public List<BobaTeaHandler> nearbyTables = new();
    private readonly string tableString = "Table";
    
    public override void Interact(NewInteract newInteract)
    {
        playerInteractRef = newInteract;
        
        if (isHeld)
        {
            if (nearbyTables.Count != 0)
            {
                Serve(playerInteractRef);
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

    public override void Serve(NewInteract newInteract)
    {
        //Add serving functionality
        FindClosestTable().Interact(true);
        
        //Ensure the below function call is included
        playerInteractRef.NoLongerHoldingSomething();
    }

    public override void WaterOperations(NewInteract newInteract)
    {
        //Not a bucket
    }

    private BobaTeaHandler FindClosestTable()
    {
        CleanList();
        if (nearbyTables.Count == 0)
            return null;
        BobaTeaHandler closest = nearbyTables.OrderBy(x => Vector3.Distance(transform.position, x.transform.position))
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
            nearbyTables.Add(other.GetComponent<BobaTeaHandler>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tableString))
        {
            nearbyTables.Remove(other.GetComponent<BobaTeaHandler>());
        }
    }
}

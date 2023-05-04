using System;
using System.Collections;
using System.Collections.Generic;
using Carl.NewInteractionSystem;
using UnityEngine;

public class Interactable_NewDirtyTea : NewAbstractInteractable
{
    private NewBobaTeaHandler tableRef;
    public bool dirtyDishOnTable;
    
    public override void Interact(NewInteract newInteract)
    {
        playerInteractRef = newInteract;
        
        if (isHeld)
        {
            Throw(playerInteractRef);
        }
        else
        {
            Hold(playerInteractRef);
        }
    }

    public override void Throw(NewInteract newInteract)
    {
        //Implement throw functionality
        
        //Ensure the below function call is included
        playerInteractRef.NoLongerHoldingSomething();
    }

    public void AddTableRef(NewBobaTeaHandler newBobaTeaHandler)
    {
        tableRef = newBobaTeaHandler;
        dirtyDishOnTable = true;
    }
    
    public override void TeaOperations(NewInteract newInteract)
    {
        //Not servable (gross)
    }

    public override void WaterOperations(NewInteract newInteract)
    {
        //Not a bucket
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pond"))
        {
            DestroyDish();
        }
    }

    private void DestroyDish()
    {
        Destroy(gameObject);
    }
}

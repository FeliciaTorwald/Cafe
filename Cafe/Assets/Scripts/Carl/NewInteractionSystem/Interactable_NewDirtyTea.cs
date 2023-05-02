using System;
using System.Collections;
using System.Collections.Generic;
using Carl.NewInteractionSystem;
using UnityEngine;

public class Interactable_NewDirtyTea : NewAbstractInteractable
{
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

    public override void Serve(NewInteract newInteract)
    {
        //Not servable (gross)
    }

    public override void WaterOperations(NewInteract newInteract)
    {
        //Not a bucket
    }
}

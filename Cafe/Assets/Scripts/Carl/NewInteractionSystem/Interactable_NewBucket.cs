using System;
using System.Collections;
using System.Collections.Generic;
using Carl.NewInteractionSystem;
using UnityEngine;

public class Interactable_NewBucket : NewAbstractInteractable
{
    private readonly string poolString = "Pond";
    private readonly string potString = "BrewingPot";
    private bool closeToWater;
    private bool closeToPot;

    public override void Interact(NewInteract newInteract)
    {
        playerInteractRef = newInteract;
        
        if (isHeld)
        {
            WaterOperations(playerInteractRef);
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
        //Not servable
    }

    public override void WaterOperations(NewInteract newInteract)
    {
        //Add water operations
        if (closeToWater)
        {
            //Add collect water functionality
        }
        else if (closeToPot)
        {
            //Add dump water to pot functionality
        }
        else
            Drop(playerInteractRef);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(poolString))
            closeToWater = true;
        if (other.CompareTag(potString))
            closeToPot = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(poolString))
            closeToWater = false;
        if (other.CompareTag(potString))
            closeToPot = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Carl.NewInteractionSystem;
using UnityEngine;

public class Interactable_NewBucket : NewAbstractInteractable
{
    private readonly string poolString = "Pond";
    private readonly string potString = "BrewingPot";

    public bool closeToWater;
    public bool closeToPot;
    public bool hasWater;

    CraftingUI canMakeTeaCheck;
    Resource addWater;

    [SerializeField] GameObject waterInBucket;
    [SerializeField] GameObject waterInKettle;

    private void Start()
    {
        addWater = FindObjectOfType<Resource>();
        canMakeTeaCheck = FindObjectOfType<CraftingUI>();
    }

    private void Update()
    {
        if (hasWater)
        {
            RemoveWater();
        }
    }
    public void RemoveWater()
    {
        var x = transform.rotation.eulerAngles.x;
        var z = transform.rotation.eulerAngles.z;

        if (x <= 91 && x >= 89 || x <= 271 && x >= 268 || z <= 91 && z >= 89 || z <= 271 && z >= 268)
        {
            hasWater = false;
            waterInBucket.gameObject.SetActive(false);
        }
    }
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

    public override void TeaOperations(NewInteract newInteract)
    {
        //Not servable
    }

    public override void WaterOperations(NewInteract newInteract)
    {
        //Add water operations
        if (closeToWater)
        {
            //Add collect water functionality
            waterInBucket.gameObject.SetActive(true);
            hasWater = true;
        }
        else if (!hasWater && closeToPot)
        {
            Drop(playerInteractRef);
        }
        else if (closeToPot)
        {
            //Add dump water to pot functionality
            if (!hasWater) { return; }
            addWater.Gather();
            hasWater = false;
            waterInBucket.gameObject.SetActive(false);
            canMakeTeaCheck.UpdateCanCraft();
            if (!waterInKettle.activeInHierarchy)
            {
                waterInKettle.gameObject.SetActive(true);
            }
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

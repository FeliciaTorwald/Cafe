using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickup : MonoBehaviour
{
    public ItemData item;

    BrewingInventory brewPot;
    Resource addWater;

    private void Start()
    {
        brewPot = FindFirstObjectByType<BrewingInventory>();
        addWater = FindObjectOfType<Resource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            brewPot.hasWater = true;
            addWater.Gather();
        }
    }
}

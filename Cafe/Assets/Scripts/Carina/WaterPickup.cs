using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickup : MonoBehaviour
{

    BrewingInventory brewPot;

    private void Start()
    {
        brewPot = FindFirstObjectByType<BrewingInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            brewPot.hasWater = true;
        }
    }
}

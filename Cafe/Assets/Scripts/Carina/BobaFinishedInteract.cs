using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaFinishedInteract : MonoBehaviour
{
    BrewingInventory pot;

    private void Start()
    {
        pot = FindFirstObjectByType<BrewingInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pot.GetComponent<BrewingInventory>().canMakeBoba = true;
            Destroy(gameObject);
        }
    }
}

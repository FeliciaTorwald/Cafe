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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                //pot.GetComponent<BrewingInventory>().canMakeBoba = true;
                //Invoke("RemoveTea", 1f);
            }
        }
    }

    private void RemoveTea()
    {
        Destroy(gameObject);
    }

   

}

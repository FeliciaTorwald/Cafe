using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPoints : MonoBehaviour
{

    BrewingInventory brewingPot;

    private void Start()
    {
        brewingPot = FindFirstObjectByType<BrewingInventory>().GetComponent<BrewingInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Net")
        {
            gameObject.SetActive(false);
            Debug.Log("Collided");
           
           FindObjectOfType<BobaCounter>().AddBoba(1);

            brewingPot.hasBoba = true;
        }

    }
}

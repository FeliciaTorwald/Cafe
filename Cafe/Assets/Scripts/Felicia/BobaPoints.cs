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
           
           FindObjectOfType<BobaCounter>().AddBoba(1);

            brewingPot.hasBoba = true;
        }

    }
}

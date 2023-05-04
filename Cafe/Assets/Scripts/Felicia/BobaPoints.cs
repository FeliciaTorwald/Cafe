using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPoints : MonoBehaviour
{
    public ItemData item;

    CraftingUI canMakeTeaCheck;
    BobaShooterController bSC;

    private void Start()
    {
        canMakeTeaCheck = FindObjectOfType<CraftingUI>();
        //bSC = FindObjectOfType<BobaShooterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Net")
        //{
        //    gameObject.SetActive(false);
           
        //   FindObjectOfType<BobaCounter>().AddBoba(1);

        //    OnInteract();
        //}

        if (other.gameObject.CompareTag("BrewingPot"))
        {
            OnInteract();
            canMakeTeaCheck.UpdateCanCraft();
            
            //bSC.IsBallFlying = false;
            //gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("BrewingPot"))
        {
            gameObject.SetActive(false);
        }
    }


    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", item.displayName);
    }

    public void OnInteract()
    {
        Inventory.instance.AddItem(item);
        //CraftingInventory.instance.AddItem(item);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{

    public ItemData itemToGive;
    public int quantityPerHit = 1;
    public int capacity;

    [SerializeField] GameObject waterInKettle;

    public void Gather()
    {
        for (int i = 0; i < quantityPerHit; i++)
        {
            Inventory.instance.AddItem(itemToGive);
            //CraftingInventory.instance.AddItem(itemToGive);
        }

    }

    public void CheckKettleForWater()
    {
        int amount = 1;

        if (Inventory.instance.HasItems(itemToGive, amount) && amount >= 1)
        {
            return;
        }
        else
        {
            if (waterInKettle.activeInHierarchy)
            {
                waterInKettle.gameObject.SetActive(false);
            }
        }
    }

}

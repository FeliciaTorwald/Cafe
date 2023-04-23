using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{

    public ItemData itemToGive;
    public int quantityPerHit = 1;
    public int capacity;

    public void Gather()
    {
        for (int i = 0; i < quantityPerHit; i++)
        {
            Inventory.instance.AddItem(itemToGive);
            CraftingInventory.instance.AddItem(itemToGive);
        }

    }

}

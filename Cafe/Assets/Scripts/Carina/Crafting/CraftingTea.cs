using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTea : MonoBehaviour
{

    BrewingInventory timerRef;

    public CraftingUI[] recipesUIs;

    public static CraftingTea instance;

    private void Awake()
    {
        instance = this;
        timerRef = FindFirstObjectByType<BrewingInventory>();
    }


    public void Craft(CraftingRecipe recipe)
    {
        for (int i = 0; i < recipe.cost.Length; i++)
        {
            for (int x = 0; x < recipe.cost[i].quantity; x++)
            {
                Inventory.instance.RemoveItem(recipe.cost[i].item);
            }
        }

        timerRef.craftQueue.Enqueue(recipe);
        timerRef.StartMakingTea(recipe);
        timerRef.isMakingTea = true;

        //Inventory.instance.AddItem(recipe.itemToCraft);

        for (int i = 0; i < recipesUIs.Length; i++)
        {
            recipesUIs[i].UpdateCanCraft();
        }
    }
}

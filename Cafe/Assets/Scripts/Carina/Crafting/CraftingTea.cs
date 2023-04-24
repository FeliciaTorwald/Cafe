using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTea : MonoBehaviour
{

    BrewingInventory timerRef;

    public CraftingRecipe recipe; //
    private bool canCraft; //

    public CraftingRecipeUI[] recipeUIs; //

    public static CraftingTea instance;

    private void Awake()
    {
        instance = this;
        timerRef = FindFirstObjectByType<BrewingInventory>();
    }


    public void UpdateCanCraft() //
    {
        canCraft = true;

        for (int i = 0; i < recipe.cost.Length; i++)
        {
            if (!Inventory.instance.HasItems(recipe.cost[i].item, recipe.cost[i].quantity))
            {
                canCraft = false;
                break;
            }
        }
        CanMakeTea();
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

        timerRef.canMakeBoba = true;
        timerRef.StartMakingTea();
        timerRef.isMakingTea = true;

        //Inventory.instance.AddItem(recipe.itemToCraft);

        for (int i = 0; i < recipeUIs.Length; i++)
        {
            recipeUIs[i].UpdateCanCraft();
        }
    }

    public void CanMakeTea() //
    {
        if (canCraft)
        {
            Craft(recipe);
        }
    }
}

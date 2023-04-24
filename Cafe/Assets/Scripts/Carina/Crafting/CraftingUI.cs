using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingUI : MonoBehaviour
{

    public CraftingRecipe recipe;


    private bool canCraft;


    public void UpdateCanCraft()
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

        CheckCanMakeTea();
    }

    public void OnClickButton()
    {
        if (canCraft)
        {
            CraftingTea.instance.Craft(recipe);
        }
    }

    public void CheckCanMakeTea()
    {
        if (canCraft)
        {
            CraftingTea.instance.Craft(recipe);
        }
    }
}

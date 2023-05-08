using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingUI : MonoBehaviour
{

    public CraftingRecipe recipe1, recipe2;

    private bool canCraft1, canCraft2;

    public void UpdateCanCraft()
    {
        canCraft1 = true;
        canCraft2 = true;

        for (int i = 0; i < recipe1.cost.Length; i++)
        {
            if (!Inventory.instance.HasItems(recipe1.cost[i].item, recipe1.cost[i].quantity))
            {
                canCraft1 = false;
                break;
            }

        }
        for (int i = 0; i < recipe2.cost.Length; i++)
        {
            if (!Inventory.instance.HasItems(recipe2.cost[i].item, recipe2.cost[i].quantity))
            {
                canCraft2 = false;
                break;
            }
        }

        FindObjectOfType<Resource>().CheckKettleForWater();
        CheckCanMakeTea();
        CheckCanMakeOtherTea();
    }

    public void OnClickButton()
    {
        if (canCraft1)
        {
            CraftingTea.instance.Craft(recipe1);
        }
        else if (canCraft2)
        {
            CraftingTea.instance.Craft(recipe2);
        }
    }

    public void CheckCanMakeTea()
    {
        if (canCraft1)
        {
            CraftingTea.instance.Craft(recipe1);
        }

    }

    public void CheckCanMakeOtherTea()
    {
        if (canCraft2)
        {
            CraftingTea.instance.Craft(recipe2);
        }
    }



    /*
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

        FindObjectOfType<Resource>().CheckKettleForWater();
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
    }*/
}

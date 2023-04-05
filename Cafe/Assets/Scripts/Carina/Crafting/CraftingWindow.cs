using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingWindow : MonoBehaviour
{
    BrewingInventory timerRef;

    public CraftingRecipeUI[] recipeUIs;

    public static CraftingWindow instance;

    private void Awake()
    {
        instance = this;
        timerRef = FindFirstObjectByType<BrewingInventory>();
    }

    private void OnOpenInventory()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Inventory.instance.onOpenInventory.AddListener(OnOpenInventory);
    }

    private void OnDisable()
    {
        Inventory.instance.onOpenInventory.RemoveListener(OnOpenInventory);
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

        //Inventory.instance.AddItem(recipe.itemToCraft);

        for (int i = 0; i < recipeUIs.Length; i++)
        {
            recipeUIs[i].UpdateCanCraft();
        }
    }
}

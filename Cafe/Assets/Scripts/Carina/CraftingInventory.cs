using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingInventory : MonoBehaviour
{

    public CraftingInventoryUI[] uiSlots;
    public CraftingSlot[] slots;

    public static CraftingInventory instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        
        slots = new CraftingSlot[uiSlots.Length];

        for (int x = 0; x < slots.Length; x++)
        {
            slots[x] = new CraftingSlot();
            uiSlots[x].index = x;
            uiSlots[x].Clear();
        }

    }

    // adds the requested item to the player's inventory
    public void AddItem(ItemData item)
    {
        if (item.canStack)
        {
            CraftingSlot slotToSTackTo = GetItemStack(item);

            if (slotToSTackTo != null)
            {
                slotToSTackTo.quantity++;
                UpdateUI();
                return;
            }
        }
        CraftingSlot emptySlot = GetEmptySlot();

        // do we have an empty slot for the item?
        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }
    }

    // updates the UI slots
    private void UpdateUI()
    {
        for (int x = 0; x < slots.Length; x++)
        {
            if (slots[x].item != null)
            {
                uiSlots[x].Set(slots[x]);
            }
            else
            {
                uiSlots[x].Clear();
            }
        }

    }

    // returns the item slot that the requested item can be stacked on
    // returns null if there is no stack available
    CraftingSlot GetItemStack(ItemData item)
    {
        for (int x = 0; x < slots.Length; x++)
        {
            if (slots[x].item == item && slots[x].quantity < item.maxStackAmount)
            {
                return slots[x];
            }
        }

        return null;
    }

    // returns an empty slot in the inventory
    // if there are no empty slots - return null
    CraftingSlot GetEmptySlot()
    {
        for (int x = 0; x < slots.Length; x++)
        {
            if (slots[x].item == null)
            {
                return slots[x];
            }
        }

        return null;
    }

    // remove item from inventory
    public void RemoveItem(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item)
            {
                slots[i].quantity--;

                if (slots[i].quantity == 0)
                {
                    slots[i].item = null;
                }

                UpdateUI();
                return;
            }
        }
    }

    // does the player have "quantity" amount of "item"s?
    public bool HasItems(ItemData item, int quantity)
    {
        int amount = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item)
            {
                amount += slots[i].quantity;
            }

            if (amount >= quantity)
            {
                return true;
            }
        }

        return false;
    }
}

public class CraftingSlot
{
    public ItemData item;
    public int quantity;
}

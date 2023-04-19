using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{

    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform dropPosition;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    PlayerMovement player;

    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        player = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        inventoryWindow.gameObject.SetActive(false);

        slots = new ItemSlot[uiSlots.Length];

        for (int x = 0; x < slots.Length; x++)
        {
            slots[x] = new ItemSlot();
            uiSlots[x].index = x;
            uiSlots[x].Clear();
        }

    }

    // when pushing inventory button (here "I"), it opens or closes inventory window
    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Toggle();
        }
    }

    // opens or closes the inventory
    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory.Invoke();
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory.Invoke();
        }
    }

    // adds the requested item to the player's inventory
    public void AddItem(ItemData item)
    {
        if (item.canStack)
        {
            ItemSlot slotToSTackTo = GetItemStack(item);

            if (slotToSTackTo != null)
            {
                slotToSTackTo.quantity++;
                UpdateUI();
                return;
            }
        }
        ItemSlot emptySlot = GetEmptySlot();

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
    ItemSlot GetItemStack(ItemData item)
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
    ItemSlot GetEmptySlot()
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

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}

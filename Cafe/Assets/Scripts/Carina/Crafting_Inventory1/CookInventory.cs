using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookInventory : MonoBehaviour
{
    [SerializeField] List<TeaItem> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }

        RefreshUI();
    }

    private void RefreshUI()
    {

        int i = 0;

        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].item = items[i];
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].item = null;
        }
    }

    public bool AddÍtem(TeaItem teaItem)
    {
        if (IsFull())
        {
            return false;
        }
        items.Add(teaItem);
        RefreshUI();
        return true;
    }

    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }

    public bool RemoveItem(TeaItem teaItem)
    {
        if (items.Remove(teaItem))
        {
            RefreshUI();
            return true;
        }
        return false;
    }
}

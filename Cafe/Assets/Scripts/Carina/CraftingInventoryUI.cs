using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingInventoryUI : MonoBehaviour
{

    public Image icon;
    public TextMeshProUGUI quantityText;
    private CraftingSlot curSlot;

    public int index;

    public void Set(CraftingSlot cSlot)
    {
        curSlot = cSlot;

        icon.gameObject.SetActive(true);
        icon.sprite = cSlot.item.icon;
        quantityText.text = cSlot.quantity > 1 ? cSlot.quantity.ToString() : string.Empty;
    }

    public void Clear()
    {
        curSlot = null;

        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

}



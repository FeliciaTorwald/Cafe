using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI quantityText;
    private ItemSlot curSlot;
    private Outline outline;

    public int index;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void Set(ItemSlot slot)
    {
        curSlot = slot;

        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
        quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;
    }

    public void Clear()
    {
        curSlot = null;

        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

}

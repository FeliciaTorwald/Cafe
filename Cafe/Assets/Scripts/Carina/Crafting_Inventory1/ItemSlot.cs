using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private TeaItem _item;
    public TeaItem item
    {
        get { return _item; }
        set { _item = value;
        if (_item == null)
            {
                image.enabled = false;
            }
        else
            {
                image.sprite = _item.ItemIcon;
                image.enabled = true;
            }
        }
    }
    [SerializeField] Image image;


    private void OnValidate()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }
}

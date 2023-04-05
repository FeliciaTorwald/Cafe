using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    // add to item to pickup, like boba, water etc

    public ItemData item;

    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", item.displayName);
    }

    public void OnInteract()
    {

        Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }

}

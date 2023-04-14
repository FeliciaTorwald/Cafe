using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPoints : MonoBehaviour
{
    public ItemData item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Net")
        {
            gameObject.SetActive(false);
           
           FindObjectOfType<BobaCounter>().AddBoba(1);

            OnInteract();
        }

        if (other.gameObject.CompareTag("BrewingPot"))
        {
            Inventory.instance.AddItem(item);
        }
    }
    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", item.displayName);
    }

    public void OnInteract()
    {
        Inventory.instance.AddItem(item);
    }
}

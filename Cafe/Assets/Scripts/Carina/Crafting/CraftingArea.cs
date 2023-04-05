using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingArea : MonoBehaviour, IInteractable
{

    private CraftingWindow craftingWindow;
    private PlayerMovement player;

    private void Start()
    {
        craftingWindow = FindObjectOfType<CraftingWindow>(true);
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnInteract();
        }
    }

    public string GetInteractPrompt()
    {
        return "Craft";
    }

    public void OnInteract()
    {
        craftingWindow.gameObject.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            craftingWindow.gameObject.SetActive(false);
        }
    }
}

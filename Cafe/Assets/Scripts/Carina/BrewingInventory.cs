using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BrewingInventory : MonoBehaviour
{
    public bool hasBoba;
    public bool canMakeBoba = true;

    private int boba = 0;

    [SerializeField] GameObject finishedTea;
    [SerializeField] GameObject spawnTeaPos;
    [SerializeField] TextMeshProUGUI addItemText;


    private void OnCollisionEnter(Collision collision)
    {
        if (hasBoba)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                addItemText.gameObject.SetActive(true);
                boba++;
                hasBoba = false;
                Invoke("HideInteractText", 0.5f);
            }
        }

        if (boba >= 2 && canMakeBoba)
        {
            BobaTea();
            canMakeBoba = false;
        }
    }

    private void BobaTea()
    {
        Instantiate(finishedTea, spawnTeaPos.transform.position, Quaternion.identity);
        boba = 0;
    }

    private void HideInteractText()
    {
        addItemText.gameObject.SetActive(false);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            addItemText.gameObject.SetActive(false);
        }
    }
}

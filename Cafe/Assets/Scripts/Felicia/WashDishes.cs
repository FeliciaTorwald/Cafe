using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashDishes : MonoBehaviour
{
    bool inTriggerArea;
    EquipTool eT;

    private void Start()
    {
        eT = FindFirstObjectByType<EquipTool>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inTriggerArea)
            {
                eT.equipped = false;
                Destroy(gameObject);
                //FindObjectOfType<CleanDishes>().Spawn();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pond"))
        {
            inTriggerArea = true;
            Debug.Log(inTriggerArea);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pond"))
        {
            inTriggerArea = false;
            Debug.Log(inTriggerArea);
        }
    }
}

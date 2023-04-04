using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashDishes : MonoBehaviour
{

    bool inTriggerArea;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (inTriggerArea)
            {
                FindObjectOfType<BobaTeaHandler>().WashDishes();
                Debug.Log("Washed");
                inTriggerArea = false;
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

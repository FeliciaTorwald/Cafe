using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BobaTeaHandler : MonoBehaviour
{
    //add this script to tables
    public GameObject fakefullBobaTea;
    public GameObject emptyBobaTea;
    public int timeConsumedTea = 1;

    bool inTriggerArea;

    private void ServedTea()
    {
        GameObject tea = Instantiate(fakefullBobaTea, transform.position, Quaternion.identity) as GameObject;
        //Invoke("DrinkingTea",timeConsumedTea);
        Destroy(tea,1f);
        //Invoke ("FinishedTea,",2);
    }
  
    private void FinishedTea()
    {
        Instantiate(emptyBobaTea, transform.position, Quaternion.identity);

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (inTriggerArea == true)
            {
                FindObjectOfType<BrewingInventory>().RemoveBobaTea();
                ServedTea();
                Invoke("FinishedTea", 2);
                //FinishedTea();
                Debug.Log("intriggerarea true");
                inTriggerArea = false;
            }
         

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boba"))
        {
            inTriggerArea = true;
            Debug.Log(inTriggerArea);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boba"))
        {
            inTriggerArea = false;
            Debug.Log(inTriggerArea);
        }
    }
}

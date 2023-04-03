using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BobaTeaHandler : MonoBehaviour
{
    //add this script to tables
    public GameObject fullBobaTea;
    public GameObject emptyBobaTea;
    public int timeConsumedTea = 1;

    private void ServedTea()
    {
        GameObject tea = Instantiate(fullBobaTea, transform.position, Quaternion.identity) as GameObject;
        //Invoke("DrinkingTea",timeConsumedTea);
        Destroy(tea);
    }
    private void DrinkingTea()
    {
        FinishedTea();
    }
    private void FinishedTea()
    {
        Instantiate(emptyBobaTea, transform.position, Quaternion.identity);

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            Debug.Log("coilldingBobaHandler");
            if (other.tag == ("Boba"))
            {
                FindObjectOfType<BrewingInventory>().BobaTea();
                ServedTea();
                FinishedTea();
            }

        }
    }

}

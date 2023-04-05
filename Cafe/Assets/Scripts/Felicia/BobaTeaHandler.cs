using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BobaTeaHandler : MonoBehaviour
{
    //add this script to tables
    public GameObject fakefullBobaTea;
    public GameObject emptyBobaTea;
    GameObject emptyTea;
    public GoldSpawner gS;
    public Chair chairRef;
    public Guest guestRef;
    bool inTriggerArea;

    private void ServedTea()
    {
        GameObject tea = Instantiate(fakefullBobaTea, transform.position, Quaternion.identity) as GameObject;
        Destroy(tea,1f);
    }

    private void FinishedTea()
    {
        if (emptyTea == null)
        {
          emptyTea = Instantiate(emptyBobaTea, transform.position, Quaternion.identity);
          guestRef.guestInteraction.ServeGuest(TeaType.TypeA);
        }
    }
    public void WashDish()
    {
        if (emptyTea != null)
        {
            Destroy(emptyTea,0.5f);
            Debug.Log("Washing");
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (inTriggerArea)
            {
                FindObjectOfType<BrewingInventory>().RemoveBobaTea();
                ServedTea();
                Invoke("FinishedTea", 2);
                gS.onOrderFullfilled = true;
                gS.Spawn();
                FindObjectOfType<GuestInteraction>().ServeGuest(TeaType.TypeA);
                inTriggerArea = false;
                gS.onOrderFullfilled = false;
            }

        }

    }

    public void AddGuestToTeaOrder(Guest guest)
    {
        guestRef = guest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boba"))
        {
            inTriggerArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boba"))
        {
            inTriggerArea = false;
        }
    }
}

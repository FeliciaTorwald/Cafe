using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static UnityEditor.Experimental.GraphView.GraphView;

public class BobaTeaHandler : Interactable
{
    //add this script to tables
    public GameObject fakefullBobaTea;
    public GameObject emptyBobaTea;
    GameObject emptyTea;
    public GoldSpawner gS;
    public Chair chairRef;
    public Guest guestRef;
    public bool inTriggerArea;
    EquipTool eT;
    public Transform dishPlace;
    private Vector3 spawnPointRef;

    OrderImageUI orderImg;

    public override void Interact()
    {
        eT = FindFirstObjectByType<EquipTool>();
        GameObject equipedBobaDrink = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<EquipTool>().gameObject;

        FindObjectOfType<BrewingInventory>().RemoveBobaTea(equipedBobaDrink);
        ServedTea();
        FindObjectOfType<PickupManager>().NoLongerHoldingSomething();
        Invoke("FinishedTea", 2);
        //gS.onOrderFullfilled = true;
        gS.Invoke(nameof(gS.Spawn), 1);
        if (guestRef != null)
        {
            if (guestRef.GetComponentInChildren<GuestInteraction>() != null)
            {
                guestRef.GetComponentInChildren<GuestInteraction>().ServeGuest(TeaType.TypeA);
            }

            if (guestRef.GetComponentInChildren<GuestInteraction>() == null)
            {
                Debug.Log("Null");
            }
        }

        inTriggerArea = false;
        gS.onOrderFullfilled = false;
    }
    private void Start()
    {
        spawnPointRef = dishPlace.transform.position;
        orderImg = FindObjectOfType<OrderImageUI>();
    }

    private void ServedTea()
    {
        GameObject tea = Instantiate(fakefullBobaTea, spawnPointRef, Quaternion.identity);
        Destroy(tea, 1f);
        orderImg.RemoveOrderImage();
    }

    private void FinishedTea()
    {
        if (emptyTea == null)
        {
            emptyTea = Instantiate(emptyBobaTea, spawnPointRef, Quaternion.identity);
            if(guestRef != null)
            {
            guestRef.guestInteraction.ServeGuest(TeaType.TypeA);
            }
        }
    }

    private void SpawnDish()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            emptyTea = Instantiate(emptyBobaTea, spawnPointRef, Quaternion.identity);
        }
    }
    public void DestroyDish()
    {     
        Destroy(emptyTea);       
    }
    public void WashDish()
    {
        if (emptyTea != null)
        {
            Destroy(emptyTea, 0.5f);
        }
    }

    private void Update()
    {
        SpawnDish();
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Boba"))
        {
            inTriggerArea = false;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBobaTeaHandler : MonoBehaviour
{
    [SerializeField] private GameObject fakeFullBobaTea;
    [SerializeField] private GameObject emptyBobaTea;
    [SerializeField] private Chair chairRef;
    [SerializeField] public Guest guestRef;
    public Transform dishPlace;
    private Vector3 spawnPointRef;
    private GameObject emptyTea;
    private GoldSpawner goldSpawner;

    public bool hasDirtyDish;

    private OrderImageUI orderImg;

    private void Start()
    {
        spawnPointRef = dishPlace.transform.position;
        orderImg = FindObjectOfType<OrderImageUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnDish();
        }
    }
    
    public void ServeTable(NewInteract newInteract, Interactable_NewFullTea tea)
    {
        Destroy(tea, .1f);
        ServedTea();
        Invoke(nameof(FinishedTea), 2f);
        goldSpawner.Invoke(nameof(goldSpawner.Spawn), 1f);
        if (guestRef != null)
        {
            guestRef.GetComponentInChildren<GuestInteraction>().ServeGuest(TeaType.TypeA);

            if (guestRef.GetComponentInChildren<GuestInteraction>() == null)
            {
                Debug.Log("Null, något är fel med GuestInteraction");
            }
        }
    }

    public void TakeOrder(NewInteract newInteract)
    {
        guestRef.guestInteraction.TakeOrder();
    }


    private void ServedTea()
    {
        GameObject tea = Instantiate(fakeFullBobaTea, spawnPointRef, Quaternion.identity);
        Destroy(tea, 1f);
        orderImg.RemoveOrderImage();
    }

    private void FinishedTea()
    {
        if (emptyTea == null)
        {
            emptyTea = Instantiate(emptyBobaTea, spawnPointRef, Quaternion.identity);
            emptyTea.GetComponent<Interactable_NewDirtyTea>().AddTableRef(this);
            hasDirtyDish = true;
            if(guestRef != null)
            {
                guestRef.guestInteraction.ServeGuest(TeaType.TypeA);
            }
        }
    }

    private void SpawnDish()
    {
        emptyTea = Instantiate(emptyBobaTea, spawnPointRef, Quaternion.identity);
    }
}

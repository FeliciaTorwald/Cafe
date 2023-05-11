using System;
using System.Collections;
using System.Collections.Generic;
using Carl.NewInteractionSystem;
using UnityEngine;

public class NewBobaTeaHandler : NewAbstractInteractable
{
    [SerializeField] private GameObject fakeFullBobaTea;
    [SerializeField] private GameObject emptyBobaTea;
    [SerializeField] private Chair chairRef;
    [SerializeField] public Guest guestRef;
    public Transform dishPlace;
    private Vector3 spawnPointRef;
    private GameObject emptyTea;
    public GoldSpawner goldSpawner;

    public bool hasDirtyDish;

    private OrderImageUI orderImg;

    SoundManager soundManager;

    private void Start()
    {
        spawnPointRef = dishPlace.transform.position;
        orderImg = FindObjectOfType<OrderImageUI>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnDish();
        }
    }
    
    public void ServeTable(NewInteract newInteract, GameObject tea, TeaType type)
    {
        if (type == guestRef.teaType)
        {
            Destroy(tea, .1f);
            ServedTea();
            soundManager.Serve();
            Invoke(nameof(FinishedTea), 2f);
            goldSpawner.Invoke(nameof(goldSpawner.Spawn), 1f);
            if (guestRef != null)
            {
                guestRef.GetComponentInChildren<GuestInteraction>().ServeGuest(type);

                if (guestRef.GetComponentInChildren<GuestInteraction>() == null)
                {
                    Debug.Log("Null, något är fel med GuestInteraction");
                }
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
    
    public void AddGuestToTeaOrder(Guest guest)
    {
        guestRef = guest;
    }

    public override void Interact(NewInteract newInteract)
    {
        if (guestRef && guestRef.stateMachine.currentState is GuestStateID.AtTable)
            TakeOrder(newInteract);
    }

    public override void Throw(NewInteract newInteract)
    {
        //Not throwable
    }

    public override void TeaOperations(NewInteract newInteract)
    {
        //Not tea
    }

    public override void WaterOperations(NewInteract newInteract)
    {
        //Not a bucket
    }
}

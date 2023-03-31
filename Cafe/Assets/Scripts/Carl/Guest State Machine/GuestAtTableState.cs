using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class GuestAtTableState : GuestState
{
    public bool served;
    
    public GuestStateID GetID()
    {
        return GuestStateID.AtTable;
    }

    public void Enter(Guest guest)
    {
        Debug.Log("Switched to At table state");
        ShowOrder(guest);
        Order();
    }

    private void Order()
    {
        //TODO: Implement functionality for when the guest orders
    }


    public void Update(Guest guest)
    {
        guest.guestCanvas.transform.forward = guest.camera.transform.forward;
        //Rotate the ordering canvas to always angle towards the camera
    }

    public void Exit(Guest guest)
    {
        Debug.Log("Left At table state");
    }
    
    private void ShowOrder(Guest guest)
    {
        guest.orderText.SetText("Tea 1");
        //Display what the guest has ordered.
    }
    
    
}

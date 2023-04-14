using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class GuestAtTableState : GuestState
{
    public GuestStateID GetID()
    {
        return GuestStateID.AtTable;
    }

    public void Enter(Guest guest)
    {
        Debug.Log("Switched to At table state");
        ShowOrder(guest);
        Order(guest);
    }

    private void Order(Guest guest)
    {
        guest.stateMachine.ChangeState(GuestStateID.Ordered);
    }

    public void Update(Guest guest)
    {
        guest.guestCanvas.transform.forward = guest.camera.transform.forward;
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

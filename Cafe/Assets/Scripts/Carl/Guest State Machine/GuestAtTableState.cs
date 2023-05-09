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
        ShowOrder(guest);
        guest.guestInteraction.ToggleAngerMeter(true);
        guest.animator.SetBool("Moving", false);
    }

    private void Order(Guest guest)
    {
        
    }

    public void Update(Guest guest)
    {
        guest.guestCanvas.transform.forward = guest.camera.transform.forward;
    }

    public void Exit(Guest guest)
    {
        guest.guestInteraction.irritation /= 2f;
    }
    
    private void ShowOrder(Guest guest)
    {
        guest.chairRef.tableRef.AddGuestToTeaOrder(guest);
        guest.orderText.SetText("Ready to order!");
        //Display what the guest has ordered.
    }
}

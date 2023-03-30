using System.Collections;
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
    }
}

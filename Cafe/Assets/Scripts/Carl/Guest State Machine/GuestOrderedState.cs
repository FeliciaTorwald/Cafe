using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestOrderedState : GuestState
{
    public GuestStateID GetID()
    {
        return GuestStateID.Ordered;
    }

    public void Enter(Guest guest)
    {
        guest.chairRef.tableRef.AddGuestToTeaOrder(guest);
        
    }

    public void Update(Guest guest)
    {
        guest.guestCanvas.transform.forward = guest.camera.transform.forward;
    }

    public void Exit(Guest guest)
    {
        
    }
}

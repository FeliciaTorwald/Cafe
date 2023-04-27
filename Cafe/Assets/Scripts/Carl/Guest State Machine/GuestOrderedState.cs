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
        AddOrder(guest);
    }

    public void Update(Guest guest)
    {
        guest.guestCanvas.transform.forward = guest.camera.transform.forward;
    }

    public void Exit(Guest guest)
    {
        
    }

    private void AddOrder(Guest guest)
    {
        
        guest.orderImg.SpawnOrderImage();
        guest.orderText.SetText("Ordered Tea 1");
    }
}

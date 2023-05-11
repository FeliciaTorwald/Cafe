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
        guest.orderTextFrameElement.SetActive(false);
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
        
        guest.orderImg.SpawnOrderImage(guest.teaType, guest);
        //guest.orderText.SetText("Ordered Tea 1");
        guest.teaOrderImg.gameObject.SetActive(true);
        guest.orderText.SetText(" ");

    }
}

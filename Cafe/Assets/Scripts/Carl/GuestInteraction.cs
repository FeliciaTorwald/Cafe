using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestInteraction : MonoBehaviour
{
    public Guest parentGuest;
    public BobaTeaHandler table;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            ServeGuest(TeaType.TypeA);
    }

    public void ServeGuest(TeaType teaType)
    {
        //Add check for whether the correct tea is served or not
        if (teaType == parentGuest.teaType && parentGuest.stateMachine.currentState == GuestStateID.Ordered)
            parentGuest.stateMachine.ChangeState(GuestStateID.Served);
    }
}

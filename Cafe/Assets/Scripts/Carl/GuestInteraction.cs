using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestInteraction : MonoBehaviour
{
    public Guest parentGuest;
    
    public void GuestInteractionExampleFunction(TeaType teaType)
    {
        if (parentGuest.teaType == teaType && parentGuest.stateMachine.currentState == GuestStateID.Ordered)
        {
            //If the right tea is served, change guest state to served
            parentGuest.stateMachine.ChangeState(GuestStateID.Served);
        }
        else
        {
            //Wrong tea is served, something else happens
        }
    }
}

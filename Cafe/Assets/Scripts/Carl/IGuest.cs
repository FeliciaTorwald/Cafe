using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGuest
{
    enum GuestType
    {
        TypeA,
        TypeB,
        TypeC
    }
    
    void OnSpawn();
    void OnAtDoor();
    void OnAtTable();
    void OnOrder();
    void OnOrderFulfilled();
    void OnFinished();
    void OnLeaving();
    Guest GetGuestObject();
}

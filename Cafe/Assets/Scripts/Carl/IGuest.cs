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
    
    void OnSpawn(int guestType);
    void OnOrder();
    void OnOrderFulfilled();
    void OnFinished();
    void OnLeaving();
}

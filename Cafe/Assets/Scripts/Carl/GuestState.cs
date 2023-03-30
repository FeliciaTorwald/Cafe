using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GuestStateID
{
    Arrived,
    AtDoor,
    AtTable,
    Ordered,
    Served,
    Leaving
}

public interface GuestState
{
    GuestStateID GetID();
    void Enter(Guest guest);
    void Update(Guest guest);
    void Exit(Guest guest);
}

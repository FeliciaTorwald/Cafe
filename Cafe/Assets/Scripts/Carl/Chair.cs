using System.Collections;
using System.Collections.Generic;
using Carl.NewInteractionSystem;
using UnityEngine;

public class Chair : MonoBehaviour, ISeat
{
    public NewBobaTeaHandler tableRef;
    public Transform seatPosition;
    
    void Start()
    {
        Invoke(nameof(AddSelf), .1f);
        tableRef = GetComponentInChildren<NewBobaTeaHandler>();
    }

    public void AddSelf()
    {
        GameManager.Instance.freeSeatsInScene.Add(this);
        GameManager.Instance.freeSeats++;
    }
    
    public Chair GetChairRef()
    {
        return this;
    }

    public void AddGuestToTable(Guest guest)
    {
        tableRef.AddGuestToTeaOrder(guest);
    }

    public bool HasDirtyDish()
    {
        return tableRef.hasDirtyDish;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour, ISeat
{
    public BobaTeaHandler tableRef;
    
    void Start()
    {
        Invoke(nameof(AddSelf), .1f);
        tableRef = GetComponentInChildren<BobaTeaHandler>();
    }
    
    void Update()
    {
        
    }

    public void AddSelf()
    {
        GameManager.Instance.freeSeatsInScene.Add(this);
        GameManager.Instance.freeSeats++;
    }
    
    public Chair GetGameObject()
    {
        return this;
    }

    public void AddGuestToTable(Guest guest)
    {
        tableRef.AddGuestToTeaOrder(guest);
    }
}

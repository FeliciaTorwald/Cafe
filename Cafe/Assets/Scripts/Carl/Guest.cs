using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour, IGuest
{
    [SerializeField] private List<Material> guestMaterial;

    public enum GuestType
    {
        TypeA,
        TypeB,
        TypeC
    }

    public GuestType guestType;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void OnSpawn()
    {
        
    }

    public void OnOrder()
    {
        
        
    }

    public void OnOrderFulfilled()
    {
        
    }

    public void OnFinished()
    {
        
    }

    public void OnLeaving()
    {
        
    }
}

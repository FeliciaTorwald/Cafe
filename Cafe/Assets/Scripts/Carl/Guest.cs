using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour, IGuest
{
    [SerializeField] private List<Mesh> guestModel;

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

    public void OnSpawn(int guestType)
    {
        switch (guestType)
        {
            case 1:
                this.guestType = GuestType.TypeA;
                break;
            case 2:
                this.guestType = GuestType.TypeB;
                break;
            case 3:
                this.guestType = GuestType.TypeC;
                break;
        }
        
        ChangeModel(guestType);
    }

    private void ChangeModel(int guestType)
    {
        switch (guestType)
        {
            case 1:
                gameObject.GetComponent<MeshFilter>().mesh = guestModel[0];
                break;
            case 2:
                gameObject.GetComponent<MeshFilter>().mesh = guestModel[1];
                break;
            case 3:
                gameObject.GetComponent<MeshFilter>().mesh = guestModel[2];
                break;
        }
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

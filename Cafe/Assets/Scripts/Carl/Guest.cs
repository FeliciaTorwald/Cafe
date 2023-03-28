using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour, IGuest
{
    [SerializeField] private List<Mesh> guestModel;
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

    public void OnSpawn(int guestID)
    {
        switch (guestID)
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
        
        ChangeModel(guestID);
    }

    private void ChangeModel(int guestID)
    {
        switch (guestID)
        {
            case 1:
                gameObject.GetComponent<MeshRenderer>().material = guestMaterial[0];
                // gameObject.GetComponent<MeshFilter>().mesh = guestModel[0];
                break;
            case 2:
                gameObject.GetComponent<MeshRenderer>().material = guestMaterial[1];
                // gameObject.GetComponent<MeshFilter>().mesh = guestModel[1];
                break;
            case 3:
                gameObject.GetComponent<MeshRenderer>().material = guestMaterial[2];
                // gameObject.GetComponent<MeshFilter>().mesh = guestModel[2];
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

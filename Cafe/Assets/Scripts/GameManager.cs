using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GuestSpawner guestSpawner;
    public List<ISeat> seatsInScene = new();
    public int freeSeats;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // public ISeat FindFreeSeat()
    // {
    //     
    // }
}

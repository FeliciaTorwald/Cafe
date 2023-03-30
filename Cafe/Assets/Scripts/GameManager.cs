using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GuestSpawner guestSpawner;
    public List<ISeat> seatsInScene = new();
    public int freeSeats = 0;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public Chair AssignSeat()
    {
        freeSeats--;
        return seatsInScene[Random.Range(0, seatsInScene.Count-1)].GetGameObject();
    }
}

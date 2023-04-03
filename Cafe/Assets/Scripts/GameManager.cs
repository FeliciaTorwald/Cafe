using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //Use "GameManager.Instance" to reference functions and variables in the GM.
    
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static GameManager instance;
    
    [SerializeField] private GuestSpawner guestSpawner;
    public int maxGuests;
    public float guestSpawnInterval;
    
    public List<ISeat> freeSeatsInScene = new();
    //Maintains a list of seats that are in the scene
    
    public int freeSeats = 0;
    //Shows how many free seats in the scene, added to by the Seats upon play start.
    
    public List<Guest> guestsInScene = new();
    //Maintains a list of all guests in the scene, added to by the GuestSpawner object.
    
    public int servedGuests;


    private void Update()
    {
        // if (freeSeats >= guestsInScene.Count)
        // {
        //     guestSpawner.SpawnNewGuest();
        // }
    }

    public Chair AssignSeat()
    {
        int chosenSeat = Random.Range(0, freeSeatsInScene.Count - 1);
        Chair chosenRef = freeSeatsInScene[chosenSeat].GetGameObject();
        freeSeatsInScene.RemoveAt(chosenSeat);
        freeSeats--;

        return chosenRef;

        //Gives a reference to a free seat to a guest upon request.
    }

    public void ReturnFreeSeat(Chair chair)
    {
        freeSeatsInScene.Add(chair);
    }

    public void AddServedGuest()
    {
        servedGuests++;
    }

    public void AddGuest(Guest guest)
    {
        guestsInScene.Add(guest);
    }

    public void RemoveGuest(Guest guest)
    {
        guestsInScene.Remove(guest);
    }
    
    public Guest GetGuest(int index)
    {
        return guestsInScene[index];
        
        //Can be used to look up a specific guest or trigger something in a random guest.
    } 
}

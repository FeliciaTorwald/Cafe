using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public List<ISeat> seatsInScene = new();
    //Maintains a list of seats that are in the scene
    
    public int freeSeats = 0;
    //Shows how many free seats in the scene, added to by the Seats upon play start.
    
    public List<Guest> guestsInScene = new();
    //Maintains a list of all guests in the scene, added to by the GuestSpawner object.
    
    public Chair AssignSeat()
    {
        freeSeats--;
        return seatsInScene[Random.Range(0, seatsInScene.Count-1)].GetGameObject();
        
        //Gives a reference to a free seat to a guest upon request.
    }

    public Guest GetGuest(int index)
    {
        return guestsInScene[index];
        
        //Can be used to look up a specific guest or trigger something in a random guest.
    } 
}

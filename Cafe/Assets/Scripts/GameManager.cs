using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    public int freeSeats = 0;

    public Chair AssignSeat()
    {
        freeSeats--;
        return seatsInScene[Random.Range(0, seatsInScene.Count-1)].GetGameObject();
    }
}

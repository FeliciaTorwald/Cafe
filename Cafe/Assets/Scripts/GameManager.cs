using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

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
    
    [SerializeField] public Difficulty difficulty;
    [SerializeField] private GuestSpawner guestSpawner;
    [SerializeField] private GameUI gameUiRef;
    [SerializeField] private int easyGuestNumber = 3;
    [SerializeField] private int normalGuestNumber = 5;
    [SerializeField] private int hardGuestNumber = 9;
    [SerializeField] private int GuestsServedToWin;
    
    private float timeSinceStart;
    private int maxNumberOfActiveGuests;
    
    public List<ISeat> freeSeatsInScene = new();
    //Maintains a list of seats that are in the scene
    
    public int freeSeats = 0;
    //Shows how many free seats in the scene, added to by the Seats upon play start.
    
    public List<Guest> guestsInScene = new();
    //Maintains a list of all guests in the scene, added to by the GuestSpawner object.
    
    public int servedGuests;
    public int earnedGold;
    public int angryGuests;


    private void Update()
    {
        if (freeSeats > 0)
        {
            guestSpawner.SpawnNewGuest();
        }

        GameTimer();
        CheckWinCondition();
    }

    private void GameTimer()
    {
        timeSinceStart += 1f * Time.deltaTime;
    }

    public int SetNumberOfGuests()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return easyGuestNumber;
            case Difficulty.Normal:
                return normalGuestNumber;
            case Difficulty.Hard:
                return hardGuestNumber;
            default:
                Debug.Log("Incorrect difficulty settings, check Game Manager");
                return 0;
        }
        
        
    }
    
    public Chair AssignSeat()
    {
        System.Random rand = new System.Random();
        for (int i = freeSeatsInScene.Count - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            ISeat temp = freeSeatsInScene[i];
            freeSeatsInScene[i] = freeSeatsInScene[j];
            freeSeatsInScene[j] = temp;
        }
        
        foreach (ISeat seat in freeSeatsInScene)
        {
            if (!seat.HasDirtyDish())
            {
                Chair chosenRef = seat.GetChairRef();
                freeSeatsInScene.Remove(seat);
                freeSeats--;
                return chosenRef;
                
            }
        }

        Debug.Log("If you see this, something has gone wrong in the AssignSeat function in the Game Manager");
        return null;
        // int chosenSeat = Random.Range(0, freeSeatsInScene.Count - 1);
        // Chair chosenRef = freeSeatsInScene[chosenSeat].GetGameObject();
        // freeSeatsInScene.RemoveAt(chosenSeat);
        // freeSeats--;
        // return chosenRef;

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

    public void AddGoldToScore(int gold)
    {
        earnedGold += gold;
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

    private void CheckWinCondition()
    {
        if (servedGuests == GuestsServedToWin)
            EndGame(true);
    }
    
    public void EndGame(bool win)
    {
        if (win)
        {
            gameUiRef.ShowEndGameUI(true, servedGuests, earnedGold, timeSinceStart);
        }
        else
        {
            gameUiRef.ShowEndGameUI(false, servedGuests, earnedGold, timeSinceStart);
        }
    }
}

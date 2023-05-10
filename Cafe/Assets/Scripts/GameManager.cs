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

public enum Day
{
    Day1,
    Day2,
    Day3
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
    [SerializeField] private GuestServedCounter guestsServed;
    [SerializeField] private int easyGuestNumber = 3;
    [SerializeField] private int normalGuestNumber = 5;
    [SerializeField] private int hardGuestNumber = 9;
    [SerializeField] private int goldEarnedToWin;
    [SerializeField] private int maxNumberOfAngryGuests;
    
    [SerializeField] private Day day;
    private float timeSinceStart;
    private int maxNumberOfActiveGuests;
    public int servedGuests;
    public int earnedGold;
    public int angryGuests;

    [Header("Day 1 settings")]
    [SerializeField] private int day1GoldToWin;
    
    [Header("Day 2 settings")]
    [SerializeField] private int day2GoldToWin;
    
    [Header("Day 3 settings")]
    [SerializeField] private int day3GoldToWin;
    

    SoundManager soundManager;

    public List<ISeat> freeSeatsInScene = new();
    //Maintains a list of seats that are in the scene

    public int freeSeats = 0;
    //Shows how many free seats in the scene, added to by the Seats upon play start.

    public List<Guest> guestsInScene = new();
    //Maintains a list of all guests in the scene, added to by the GuestSpawner object.

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        switch (day)
        {
            case Day.Day1:
                goldEarnedToWin = day1GoldToWin;
                break;
            case Day.Day2:
                goldEarnedToWin = day2GoldToWin;
                break;
            case Day.Day3:
                goldEarnedToWin = day3GoldToWin;
                break;
        }
    }
    private void Update()
    {
        if (freeSeats > 0)
        {
            guestSpawner.SpawnNewGuest();
        }

        GameTimer();
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

        //Gives a reference to a free seat to a guest upon request.
    }

    public Day CheckDay()
    {
        return day;
    }
    
    public void ReturnFreeSeat(Chair chair)
    {
        freeSeatsInScene.Add(chair);
    }

    public void AddServedGuest()
    {
        servedGuests++;
        guestsServed.ServedGuestsCheck(servedGuests);
    }

    public void AddAngryGuest()
    {
        angryGuests++;
        CheckWinCondition(false);
    }

    public void AddGoldToScore(int gold)
    {
        earnedGold += gold;
        CheckWinCondition(true);
    }

    public void AddVisitingGuest(Guest guest)
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

    private void CheckWinCondition(bool gold)
    {
        //Function to check if the player has won
        if (gold)
        {
            if (earnedGold >= goldEarnedToWin)
                EndGame(true);
        }
        else
        {
            if (angryGuests >= maxNumberOfAngryGuests)
                EndGame(false);
        }
    }

    private void EndGame(bool win)
    {
        //Triggers the endgame screen
        gameUiRef.ShowEndGameUI(win, servedGuests, earnedGold, timeSinceStart);
    }
}

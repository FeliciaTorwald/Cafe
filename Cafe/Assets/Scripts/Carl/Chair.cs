using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour, ISeat
{
    private GameManager gameManager;
    
    void Start()
    {
        Invoke(nameof(AddSelf), .1f);
    }
    
    void Update()
    {
        
    }

    public void AddSelf()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.seatsInScene.Add(this);
        gameManager.freeSeats++;

    }
}

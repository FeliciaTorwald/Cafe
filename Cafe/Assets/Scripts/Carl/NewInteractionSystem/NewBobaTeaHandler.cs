using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBobaTeaHandler : MonoBehaviour
{
    [SerializeField] private GameObject fakeFullBobaTea;
    [SerializeField] private GameObject emptyBobaTea;
    [SerializeField] private Chair chairRef;
    [SerializeField] private Guest guestRef;
    public Transform dishPlace;
    private Vector3 spawnPointRef;
    private GameObject emptyTea;
    private GoldSpawner goldSpawner;

    public bool hasDirtyDish;

    private OrderImageUI orderImg;

    public void ServeTable()
    {
        
    }

    private void Start()
    {
        spawnPointRef = dishPlace.transform.position;
        orderImg = FindObjectOfType<OrderImageUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnDish();
        }
    }

    private void ServedTea()
    {
        
    }

    private void FinishedTea()
    {
        
    }

    private void SpawnDish()
    {
        
    }

    public void DestroyDish()
    {
        
    }
}

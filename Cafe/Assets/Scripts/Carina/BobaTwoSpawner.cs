using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaTwoSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    int firstIndex;
    [SerializeField] GameObject bobaTwo;
    GameObject newBobaTwo;
    public int bobaAmount = 0;
    int minTime = 1;
    int maxTime = 8;

    bool isSpawning;


    private void Awake()
    {
        isSpawning = false;
    }

    private void Update()
    {
        if (!isSpawning && bobaAmount <= 1)
        {
            float timer = Random.Range(minTime, maxTime);
            Invoke("SpawnBoba", timer);
            isSpawning = true;
        }
    }

    private void SpawnBoba()
    {
        newBobaTwo = Instantiate(bobaTwo, spawnPoints[firstIndex].position, spawnPoints[firstIndex].rotation, transform);
        bobaAmount++;
        isSpawning = false;
    }
}

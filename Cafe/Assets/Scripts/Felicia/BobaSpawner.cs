using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BobaSpawner : MonoBehaviour
{
    public GameObject preFab;
    public float bobaInterval = 1f;
    public float bobaLifeTime;
    public int amountToPool;
    public List<GameObject> pooledObjects;
    private Vector3 spawnPointRef;
    public float maxBobaInScene = 5;
    public Transform[] spawnPoints; 
    int firstIndex; 

    private void Start()
    {
        pooledObjects = new List<GameObject>();

        //instantiates all the boba once
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject newBoba = Instantiate(preFab, spawnPoints[firstIndex].position, spawnPoints[firstIndex].rotation, transform);
            newBoba.SetActive(false);
            pooledObjects.Add(newBoba);
        }

        InvokeRepeating(nameof(Spawn), bobaInterval, bobaInterval);
    }


    private void Spawn()
    {
        firstIndex = Random.Range(0, 2);

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //if something in pooledObjects is empty activate them
            if (!pooledObjects[i].activeSelf)
            {
                pooledObjects[i].transform.position = spawnPoints[firstIndex].position;
                pooledObjects[i].GetComponent<Rigidbody>().isKinematic = false;
                pooledObjects[i].SetActive(true);
                //pooledObjects[i].GetComponent<BobaLifespan>().Spawned(bobaLifeTime);
                return;
            }
        }
        //Stops if  the maximun has been spawned
        if (pooledObjects.Count >= maxBobaInScene)
        {
            return;
        }
    }
}

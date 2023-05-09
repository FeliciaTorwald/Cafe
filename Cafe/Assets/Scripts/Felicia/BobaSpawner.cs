using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BobaSpawner : MonoBehaviour
{
    public GameObject preFab;
    public List<GameObject> pooledObjects;

    public float bobaInterval = 1f;
    public float bobaLifeTime;
    public float maxBobaInScene = 5;

    public int amountToPool;
    int firstIndex;

    private Vector3 spawnPointRef;

    public Transform[] spawnPoints;

    Interactable_NewBoba interactable_NewBoba;

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

        interactable_NewBoba = FindObjectOfType<Interactable_NewBoba>();
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
                pooledObjects[i].GetComponent<SphereCollider>().enabled = true;
                //interactable_NewBoba.isBallFlying = false;
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

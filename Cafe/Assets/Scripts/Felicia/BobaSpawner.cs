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
        StartCoroutine(SpawnBobas(bobaInterval, preFab));
    }

    private IEnumerator SpawnBobas(float interval, GameObject bobaBall)
    {
        yield return new WaitForSecondsRealtime(interval);
        for (int i = 0; i < amountToPool; i++)
        {
            Spawn();
        }
        // makes it a loop
        StartCoroutine(SpawnBobas(interval, bobaBall));
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
                pooledObjects[i].GetComponent<BobaLifespan>().Spawned(bobaLifeTime);
                return;
            }
        }
        //Stops if  the maximun has been spawned
        if (pooledObjects.Count >= maxBobaInScene)
        {
            return;
        }
        //instantiates the boba once
        GameObject newBoba = Instantiate(preFab, spawnPoints[firstIndex].position, spawnPoints[firstIndex].rotation);
        newBoba.SetActive(true);
        pooledObjects.Add(newBoba);
        newBoba.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100); //
        newBoba.GetComponent<BobaLifespan>().Spawned(bobaLifeTime);
    }
}

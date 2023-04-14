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
    public Transform playerPosRef;
    private Vector3 spawnPointRef;
    public float maxBobaInScene = 5;
    BobaShooterController bSC;
    public Transform[] spawnPoints; 
    int firstIndex; 

    private void Start()
    {
        bSC = FindFirstObjectByType<BobaShooterController>();
        pooledObjects = new List<GameObject>();
        StartCoroutine(SpawnBobas(bobaInterval, preFab));
        bSC.Balls = pooledObjects;
        //spawnPointRef = playerPosRef.position;

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

    private IEnumerator Despawn(float interval, GameObject bobaBall)
    {
        yield return new WaitForSecondsRealtime(interval);
        bobaBall.SetActive(false);

    }
    private void Spawn()
    {
        firstIndex = Random.Range(0, 2);

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //if something in pooledObjects is empty activate them
            if (!pooledObjects[i].activeSelf)
            {
                //pooledObjects[i].transform.position = new Vector3(spawnPointRef.x + Random.Range(-3f, 1), spawnPointRef.y + 18, spawnPointRef.z + Random.Range(-3f, 8));
                pooledObjects[i].transform.position = spawnPoints[firstIndex].position; 
                pooledObjects[i].SetActive(true);
                StartCoroutine(Despawn(bobaLifeTime, pooledObjects[i]));
                return;
            }
        }
        //Stops if  the maximun has been spawned
        if (pooledObjects.Count >= maxBobaInScene)
        {
            return;
        }
        //instantiates the boba once
        //GameObject newPoint = Instantiate(preFab, new Vector3(spawnPointRef.x + Random.Range(-3f, 1), spawnPointRef.y + 18, spawnPointRef.z + Random.Range(-3f, 8)), Quaternion.identity);
        GameObject newPoint = Instantiate(preFab, spawnPoints[firstIndex].position, spawnPoints[firstIndex].rotation);
        newPoint.SetActive(true);
        pooledObjects.Add(newPoint);
        bSC.Ball = newPoint;
        newPoint.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100); //
        StartCoroutine(Despawn(bobaLifeTime, newPoint));
    }
    
   
}

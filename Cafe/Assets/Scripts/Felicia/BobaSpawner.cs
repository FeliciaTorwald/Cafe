using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BobaSpawner : MonoBehaviour
{
    public GameObject preFab;
    public float bobaInterval = 1f;
    public int amountToPool;
    public List<GameObject> pooledObjects;
    public Transform playerPosRef;
    private Vector3 spawnPointRef;
    public float maxBobaInScene = 5;
    public float timer = 0;
    BobaShooterController bSC;
    
    private void Start()
    {
        bSC = FindFirstObjectByType<BobaShooterController>();
        StartCoroutine(spawnPoints(bobaInterval, preFab));
        pooledObjects = new List<GameObject>();
        bSC.Balls = pooledObjects;
        spawnPointRef = playerPosRef.position;
    }

    private IEnumerator spawnPoints(float interval, GameObject point)
    {
        yield return new WaitForSecondsRealtime(interval);
        DisableObjects();
        for (int i = 0; i < amountToPool; i++)
        {
            Spawn();
        }
        StartCoroutine(spawnPoints(interval, point));
    }

    private void Spawn()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {

            if (!pooledObjects[i].activeSelf)
            {
                pooledObjects[i].transform.position = new Vector3(spawnPointRef.x+Random.Range(-3f, 1),spawnPointRef.y+8, spawnPointRef.z+Random.Range(-3f, 8));
                pooledObjects[i].SetActive(true);
                return;
            }
        }
        
    }

    private void Update() 
    {
        //Invoke("RemoveFromList",20);
        timer += Time.deltaTime;

        if(GameObject.FindGameObjectsWithTag("BobaPearls").Length < maxBobaInScene && timer >= 1) 
        {
            GameObject newPoint = Instantiate(preFab, new Vector3(spawnPointRef.x+Random.Range(-3f, 1),spawnPointRef.y+8, spawnPointRef.z+Random.Range(-3f, 8)), Quaternion.identity);
            pooledObjects.Add(newPoint);
            bSC.Ball = newPoint;
            timer = 0;
        }
    }

    private void DisableObjects()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].transform.position.y < -15
                )
            {
                pooledObjects[i].SetActive(false);
            }
        }
    }

    //private void RemoveFromList()
    //{
    //    for (int i = 0; i < pooledObjects.Count; i++)
    //    {
    //        pooledObjects.Remove(pooledObjects[i]);
    //        //Destroy(pooledObjects[i]);
    //        break;
    //    }
    //}

}

using System.Collections;
using System.Collections.Generic;
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
    
    private void Start()
    {
        StartCoroutine(spawnPoints(bobaInterval, preFab));
        pooledObjects = new List<GameObject>();

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
        timer += Time.deltaTime;

        if(GameObject.FindGameObjectsWithTag("BobaPearls").Length < maxBobaInScene && timer >= 10) 
        {
        GameObject newPoint = Instantiate(preFab, new Vector3(spawnPointRef.x+Random.Range(-3f, 1),spawnPointRef.y+8, spawnPointRef.z+Random.Range(-3f, 8)), Quaternion.identity);
        pooledObjects.Add(newPoint);
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

}

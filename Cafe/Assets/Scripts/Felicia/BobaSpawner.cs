using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BobaSpawner : MonoBehaviour
{
    public GameObject preFab;
    public float bobaInterval = 3.5f;
    public int amountToPool;
    public List<GameObject> pooledObjects;
    

  
    private void Start()
    {
        StartCoroutine(spawnPoints(bobaInterval, preFab));
        pooledObjects = new List<GameObject>();
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
                pooledObjects[i].transform.position = new Vector3(Random.Range(-10f, 11),5, Random.Range(-10f, 11));
                pooledObjects[i].SetActive(true);
                return;

            }
        }
        GameObject newPoint = Instantiate(preFab, new Vector3(Random.Range(-10f, 11),5, Random.Range(-10f, 11)), Quaternion.identity);
        pooledObjects.Add(newPoint);
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

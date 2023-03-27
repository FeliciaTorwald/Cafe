using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

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
                pooledObjects[i].transform.position = new Vector3(Random.Range(-1f, 5), Random.Range(-1f, 5), 0);
                pooledObjects[i].SetActive(true);
                return;

            }
        }
        GameObject newPoint = Instantiate(preFab, new Vector3(Random.Range(-1f, 5), Random.Range(-1f, 5), 0), Quaternion.identity);
        pooledObjects.Add(newPoint);
    }
    private void DisableObjects()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].transform.position.y < -10)
            {
                pooledObjects[i].SetActive(false);
            }
        }
    }

}

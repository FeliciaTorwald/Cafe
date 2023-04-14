using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    public GameObject preFabGold;
    public bool onOrderFullfilled;
    GameObject coin;
    //public Transform moneyPlace;
    //private Vector3 spawnPointRef;

    public void Start()
    {
        onOrderFullfilled= false;
        //spawnPointRef = moneyPlace.position;
    }
    public void Update()
    {
        // Spawn();
        //DestroyCoin();
    }
    public void Spawn()
    {
        if (onOrderFullfilled)
        {
            coin = Instantiate(preFabGold, transform.position, Quaternion.identity);//TODO Change so that coin spawn on moneyPlace, do this when we build in the gamescene together
        }
    }

    public void DestroyCoin()
    {
        if (onOrderFullfilled == false)
        {
        Destroy(coin,0.5f);
        }
    }
}

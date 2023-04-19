using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;

public class GoldSpawner : MonoBehaviour
{
    public GameObject preFabGold;
    public bool onOrderFullfilled;
    GameObject coin;
    public Transform moneyPlace;
    private Vector3 spawnPointRef;
    float amountOfCoins = 3;
    List<GameObject> coins;

    public void Start()
    {
        spawnPointRef = moneyPlace.transform.position; 
        onOrderFullfilled = false;
        coins = new List<GameObject>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Invoke(nameof(Spawn),1);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            DestroyCoin();
        }
        //if(coin.transform.position.y <= 0)
        //{
        //    coin.GetComponent<Rigidbody>().isKinematic = true;
        //}
    }
    public void Spawn()
    {
        if (coin == null)
        {
            for (int i = 0; i < amountOfCoins; i++)
            {
                coin = Instantiate(preFabGold, spawnPointRef, Quaternion.identity);
                coin.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0) , ForceMode.Impulse);
                coins.Add(coin);
               //Invoke(nameof(TurnOffPhysics),2);
            }
        }
    }

    public void TurnOffPhysics()
    {
        coin.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void DestroyCoin()
    {
        if (coin != null)
        {
            for (int i = 0; i < coins.Count; i++)
            {
                 Destroy(coin);
            }
        }
    }

}

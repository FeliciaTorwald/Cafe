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
    float amountOfCoins = 5;
    List<GameObject> coins;
    float timer;
    SoundManager sM;

    public void Start()
    {
        spawnPointRef = moneyPlace.transform.position; 
        onOrderFullfilled = false;
        coins = new List<GameObject>();
        sM = FindObjectOfType<SoundManager>();
    }
    public void Update()
    {
        timer += Time.deltaTime;
        //devtools
        if (Input.GetKeyDown(KeyCode.M))
        {
            Invoke(nameof(Spawn),1);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            DestroyCoin();
        }
        //if (coin.transform.position.y < -3)
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
              if(timer >= 1)
                {
                sM.Coin();
                coin = Instantiate(preFabGold, spawnPointRef, Quaternion.identity);
                coin.transform.position += Random.insideUnitSphere + new Vector3(0,1,0);
                Vector3 force = Random.insideUnitSphere * 6;
                force.y = Mathf.Abs(force.y);
                coin.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
                coins.Add(coin);
                //timer = 0;
                }
                              
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

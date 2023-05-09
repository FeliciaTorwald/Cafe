using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;

public class GoldSpawner : MonoBehaviour
{
    SoundManager sM;
    GameObject coin;

    public GameObject preFabGold;

    public Transform moneyPlace;
    public Transform playerTarget;

    private Vector3 spawnPointRef;

    int amountOfCoins = 1;
    float timer;
    float t;
    public float goldInterval = 1f;

    public bool onOrderFullfilled;
    bool coinIsFlying;
    bool keepSpawningCoins;

    List<GameObject> coinList;

    public void Start()
    {
        spawnPointRef = moneyPlace.transform.position;
        coinList = new List<GameObject>();
        onOrderFullfilled = false;
        sM = FindObjectOfType<SoundManager>();
        playerTarget = GameObject.Find("ToolParent").transform;

    }
    public void Update()
    {
        timer += Time.deltaTime;
        //devtools
        if (Input.GetKeyDown(KeyCode.M))
        {
            //Invoke(nameof(Spawn),1);
            Spawn();
            //StartCoroutine(Spawn());
        }

        if (coinIsFlying)
        {
            t += Time.deltaTime;
            //Implement throwing functionality
            float duration = 1.2f;
            float t01 = t / duration;

            // move to target
            Vector3 A = moneyPlace.position;
            Vector3 B = playerTarget.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            // move in arc
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);
            coin.transform.position = pos + arc;

            if (t01 >= 0.95)
            {
                coin.GetComponent<Rigidbody>().isKinematic = false;
                coin.GetComponent<BoxCollider>().enabled = true;
                coinIsFlying = false;
                Destroy(coin);
            }

        }
    }
    public void Spawn()
    {
        //WaitForSeconds wait = new WaitForSeconds(3f);
        if (coin == null)
        {
            for (int i = 0; i < amountOfCoins; i++)
            {
                    sM.Coin();
                    coin = Instantiate(preFabGold, spawnPointRef, transform.rotation * Quaternion.Euler(180f, 180f, 0f));
                    coinList.Add(coin);
                    coinIsFlying = true;
                    t = 0;
                    //yield return wait;
            }

        }
    }
}

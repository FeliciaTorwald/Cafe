using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanDishes : MonoBehaviour
{
    public GameObject cleanTeaCup;
    bool inTriggerArea;
    public Transform spawnPosRef;
    private Vector3 spawnPointRef;
    void Start()
    {
        //spawnPosRef = GameObject.Find("SpawnDish").transform;
        //spawnPointRef = spawnPosRef.position;
    }

    public void Spawn()
    {
        Instantiate(cleanTeaCup,spawnPosRef.transform.position,Quaternion.identity);
    }
}

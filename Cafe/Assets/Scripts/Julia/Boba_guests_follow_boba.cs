using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boba_guests_follow_boba : MonoBehaviour
{
    public string tagToDetect = "BobaPearls";
    public GameObject[] allBobaPearls;
    public GameObject closestBoba;

    void Start()
    {
        allBobaPearls = GameObject.FindGameObjectsWithTag(tagToDetect);
    }

    private void Update() 
    {
        //Vector3 BobaPos = boba.transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, BobaPos, speed * Time.deltaTime);
        closestBoba = FindClosestBoba();
        print(closestBoba.name);
    }

    public GameObject FindClosestBoba()
    {
        GameObject[] Bobas;
        Bobas = GameObject.FindGameObjectsWithTag("BobaPearls");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject Boba in Bobas)
        {
            Vector3 diff = Boba.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = Boba;
                distance = curDistance;
            }
        }
        return closest;
    }
}

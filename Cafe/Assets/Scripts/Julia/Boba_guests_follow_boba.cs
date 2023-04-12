using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boba_guests_follow_boba : MonoBehaviour
{
    public string tagToDetect = "BobaPearls";
    public GameObject[] allBobaPearls;
    public GameObject closestBoba;

    bool ful_Mouth = false;
    float speed = 1.5f;
    float Boba_In_Mouth = 0;

    void Start()
    {
        allBobaPearls = GameObject.FindGameObjectsWithTag(tagToDetect);
        Boba_In_Mouth = 0;
    }

    private void Update()
    {
        closestBoba = FindClosestBoba();
        //print(closestBoba.name);  
        transform.position = Vector3.MoveTowards(transform.position, closestBoba.transform.position, speed * Time.deltaTime);
    }

    public GameObject FindClosestBoba()
    {
        GameObject[] Boba_pearls;
        Boba_pearls = GameObject.FindGameObjectsWithTag("BobaPearls");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject Boba in Boba_pearls)
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

    private void Ontrigger(Collider other) 
    {
        Debug.Log("collision");
        if (other.tag == "BobaPearls" && ful_Mouth == false)
        {
            Debug.Log("hello");
            Boba_In_Mouth += 1;
            Destroy(closestBoba);
            
            if (Boba_In_Mouth >= 5)
            {
                ful_Mouth = true;
            }
        }
    }
}

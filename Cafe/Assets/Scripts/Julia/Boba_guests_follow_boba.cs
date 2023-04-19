using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boba_guests_follow_boba : MonoBehaviour
{
    public string tagToDetect = "BobaPearls";
    public GameObject[] allBobaPearls;
    public GameObject closestBoba;
    public Transform toTheExit;

    bool ful_Mouth = false;
    float speed = 2f;
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
        if(ful_Mouth == false)
        {
        transform.position = Vector3.MoveTowards(transform.position, closestBoba.transform.position, speed * Time.deltaTime);
        }

        if(ful_Mouth == true)
        {
        transform.position = Vector3.MoveTowards(transform.position, toTheExit.position, speed * Time.deltaTime);
        }
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

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("collision");
        if (other.gameObject.tag == "BobaPearls" && ful_Mouth == false)
        {
            Debug.Log("Is boba in mounth");
            Boba_In_Mouth += 1;
            Destroy(closestBoba);
            
            if (Boba_In_Mouth >= 5)
            {
                ful_Mouth = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boba_guests_follow_boba : MonoBehaviour
{
    public string tagToDetect = "BobaPearls";
    public GameObject[] allBobaPearls;
    public GameObject closestBoba;
    public GameObject preFab;
    public Transform toTheExit;
    public Transform spat_out_boba;
    hit_boba_eating_guest boba_eating_guest_got_hit_true;

    bool ful_Mouth = false;
    float speed = 2f;
    float Boba_In_Mouth = 0;

    void Start()
    {
        boba_eating_guest_got_hit_true = FindFirstObjectByType<hit_boba_eating_guest>();

        allBobaPearls = GameObject.FindGameObjectsWithTag(tagToDetect);
        Boba_In_Mouth = 0;
    }

    private void Update()
    {
        //Debug.Log(Boba_In_Mouth);
        closestBoba = FindClosestBoba();
        //print(closestBoba.name);  
        if(ful_Mouth == false && boba_eating_guest_got_hit_true.Boba_guests_got_hit == false)
        {
        transform.position = Vector3.MoveTowards(transform.position, closestBoba.transform.position, speed * Time.deltaTime);
        }

        if(ful_Mouth == true)
        {
        transform.position = Vector3.MoveTowards(transform.position, toTheExit.position, speed * Time.deltaTime);
        }

        if(boba_eating_guest_got_hit_true.Boba_guests_got_hit == true)
        {
                for (int i = 0; i < Boba_In_Mouth; i++) 
                {
                    Debug.Log("Spawn boba");
                    Instantiate(preFab, spat_out_boba.position, Quaternion.identity);
                }
                boba_eating_guest_got_hit_true.Boba_guests_got_hit = false;
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
        if (other.gameObject.tag == "BobaPearls" && ful_Mouth == false)
        {
            Boba_In_Mouth +=1;
            Debug.Log("Is boba in mounth");
            Debug.Log(Boba_In_Mouth);
            Destroy(closestBoba);
            
            if (Boba_In_Mouth >= 5)
            {
                ful_Mouth = true;
            }
        }
    }
}

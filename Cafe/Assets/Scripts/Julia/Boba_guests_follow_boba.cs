using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boba_guests_follow_boba : MonoBehaviour
{
    public string tagToDetect = "BobaPearls";
    public GameObject[] allBobaPearls;
    public GameObject closestBoba;
    public GameObject preFab;
    public Transform toTheExit;
    public Transform spat_out_boba;
    hit_boba_eating_guest boba_eating_guest_got_hit_true;
    private NavMeshAgent nav;

    
    bool ful_Mouth = false;
    bool intriggerarea;
    bool Boba_guests_got_hit;
    float speed = 500f;
    public float Boba_In_Mouth = 0;

    void Start()
    {
        //boba_eating_guest_got_hit_true = FindFirstObjectByType<hit_boba_eating_guest>();

        //Find all bobaPearls
        allBobaPearls = GameObject.FindGameObjectsWithTag(tagToDetect);

        Boba_In_Mouth = 0;
        nav = GetComponent<NavMeshAgent>();

        //Invoke("closestBobas", 3.0f);
        InvokeRepeating(nameof(closestBobas), 1,1);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (intriggerarea == true)
            {
                Boba_guests_got_hit = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (intriggerarea == true)
            {
                Boba_In_Mouth = 0;
            }
        }

        if (Boba_guests_got_hit == true)
        {
                for (int i = 0; i < Boba_In_Mouth; i++) 
                {
                    Instantiate(preFab, spat_out_boba.position, Quaternion.identity);
                    Boba_guests_got_hit = false;
                }
                ful_Mouth = true;
        }

        //Ha inte denna i update utan kolla först på vilken är närmsta boba och sen när bobans förstörs kolla var nästa ligger
        if(closestBoba == null)
            return;
        
        //nav.destination = FindClosestBoba();

        //print(closestBoba.name);  
        if(ful_Mouth == false && Boba_guests_got_hit == false)
        {
        //nav.destination = Vector3.MoveTowards(transform.position, closestBoba.transform.position, speed * Time.deltaTime);
        nav.destination = closestBoba.transform.position;
        }

        if(ful_Mouth == true)
        {
        //nav.destination = Vector3.MoveTowards(transform.position, toTheExit.position, speed * Time.deltaTime);
        nav.destination = toTheExit.position;
        }

    }

    public GameObject FindClosestBoba()
    {
        //List over boba perls
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
    public void closestBobas()
    {
    //find first boba
    closestBoba = FindClosestBoba();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "BobaPearls" && ful_Mouth == false)
        {
            Boba_In_Mouth +=1;
            closestBoba.SetActive(false);
            //closestBoba = FindClosestBoba();
            
            if (Boba_In_Mouth >= 5)
            {
                ful_Mouth = true;
            }
        }

        if (other.gameObject.tag == "Player")
        {
            intriggerarea = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            intriggerarea = false;
        }

    }
}

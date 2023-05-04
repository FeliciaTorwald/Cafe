using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boba_guests_follow_boba : MonoBehaviour
{
    public string tagToDetect = "BobaPearls";
    public GameObject[] allBobaTeacups;
    //public List<GameObject> allBobaTeacups;
    public GameObject closestTeacup;
    public GameObject prefab_FullTeacup;
    public GameObject prefab_EmptyTeacup;
    public GameObject Boba_guests_pick_up_bobatea;
    public Transform toTheExit;
    public Transform spat_out_boba;
    hit_boba_eating_guest boba_eating_guest_got_hit_true;
    private NavMeshAgent nav;
    public float range = 10; //radius of sphere

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    bool ful_Hands = false;
    bool intriggerarea;
    bool Boba_guests_got_hit;
    float speed = 500f;
    public float Boba_In_Hand = 0;
    float timer_guestGoAroundRandom;

    public AudioSource source;
    public AudioClip hit;

    float counter;
    bool Inhands = false;

    void Start()
    {

        //boba_eating_guest_got_hit_true = FindFirstObjectByType<hit_boba_eating_guest>();

        //Find all bobaPearls
        allBobaTeacups = GameObject.FindGameObjectsWithTag(tagToDetect);

        Boba_In_Hand = 0;
        nav = GetComponent<NavMeshAgent>();

        //Invoke("closestTeacups", 3.0f);
        InvokeRepeating(nameof(closestTeacups), 1,1);
    }

    private void Update()
    {
        timer_guestGoAroundRandom += Time.deltaTime;

        for (int i = 0; i < allBobaTeacups.Length; i++)
        {
            if (allBobaTeacups[i] != null)
            {
                counter++;
            }
        }

        if (counter == 0)
        {
            WalkRandom();
            //Debug.Log("is this going random");
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (intriggerarea == true)
            {
                Boba_guests_got_hit = true;
                source.PlayOneShot(hit);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (intriggerarea == true)
            {
                Boba_In_Hand = 0;
            }
        }

        if (Boba_guests_got_hit == true)
        {
                for (int i = 0; i < Boba_In_Hand; i++) 
                {
                    closestTeacup.GetComponent<Rigidbody>().isKinematic = false;
                    Inhands = false;
                    //Instantiate(prefab_FullTeacup, spat_out_boba.position, Quaternion.identity);
                    //Boba_guests_got_hit = false;
                }
                ful_Hands = true;
        }

        if (Inhands == true)
        {
            closestTeacup.transform.position = Boba_guests_pick_up_bobatea.transform.position;
        }

        //Ha inte denna i update utan kolla först på vilken är närmsta boba och sen när bobans förstörs kolla var nästa ligger
        if(closestTeacup == null)
            return;
        
        //nav.destination = FindclosestTeacup();

        //print(closestTeacup.name);  
        if(ful_Hands == false && Boba_guests_got_hit == false)
        {
        //nav.destination = Vector3.MoveTowards(transform.position, closestTeacup.transform.position, speed * Time.deltaTime);
        nav.destination = closestTeacup.transform.position;
        }

        //Boba guest ska hitta närmaste boba tea och ta upp den och sen dricka den under 6s och sen ska den toma boba tean popa up och 
        //droppas på marken och sen ska han försöka hitta nästa närmaste boba tea för att göra samma sak.
        if(ful_Hands == true)
        {
        timer_guestGoAroundRandom = 0;
        

        if (timer_guestGoAroundRandom >= 6)
        {
        nav.destination = Vector3.MoveTowards(transform.position, toTheExit.position, speed * Time.deltaTime);
        }

        }
    }


    public GameObject FindclosestTeacup()
    {
        //List over boba perls
        GameObject[] Boba_Teacup;
        Boba_Teacup = GameObject.FindGameObjectsWithTag("Boba");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject Boba in Boba_Teacup)
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
    public void closestTeacups()
    {
    //find first Teacups
    closestTeacup = FindclosestTeacup();
    }

    public void WalkRandom()
    {
        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {

            Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
            { 
                //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
                //or add a for loop like in the documentation
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }


        if(nav.remainingDistance <= nav.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                nav.SetDestination(point);
            }
        }
    }


    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Boba" && ful_Hands == false)
        {
            Boba_In_Hand +=1;

            
            closestTeacup.GetComponent<Rigidbody>().isKinematic = true;
            Inhands = true;

            //closestTeacup.SetActive(false);
            //closestTeacup = FindclosestTeacup();
            
            if (Boba_In_Hand >= 1)
            {
                ful_Hands = true;
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

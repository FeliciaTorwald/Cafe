using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

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
    [SerializeField] private float Radius = 20;
    [SerializeField] private bool Debug_Bool;
    NavMeshAgent nav;
    Vector3 Next_pos;
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
        Next_pos = transform.position;

        InvokeRepeating(nameof(closestTeacups), 1,1);

        //generate first random position and walk to it
        Next_pos = Generate_Random_Pos.R_Pos(transform.position,Radius);
        nav.SetDestination(Next_pos);
    }

    private void Update()
    {
        //timer for when boba guest drinks tea
        timer_guestGoAroundRandom += Time.deltaTime;

        //Looks so their is no BobaTeacups in scene
        for (int i = 0; i < allBobaTeacups.Length; i++)
        {
            if (allBobaTeacups[i] != null)
            {
                counter++;
            }
        }

        //If no BobaTeacups is in scene go to an random Location (NOT DONE)
        if (counter == 0)
        {
            //SKapa bool som kollar om man kommit fram och när man kommit fram gör ny position. 
            //ev använd carls funcktion ReachedDestinationOrGaveUp i scriptet GuestArrivedState.
            Debug.Log(Vector3.Distance(Next_pos, transform.position));
            
            if(Vector3.Distance(Next_pos, transform.position) <= 13f)
            {
                Debug.Log("Framme?");
                Next_pos = Generate_Random_Pos.R_Pos(transform.position,Radius);
                nav.SetDestination(Next_pos);
            }
        }
        
        //Looks if boba guest got hit and if it has been hit it makes a sound
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (intriggerarea == true)
            {
                Boba_guests_got_hit = true;
                source.PlayOneShot(hit);
            }
        }

        //empty bobaguest hands on BobaTeacups
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (intriggerarea == true)
            {
                Boba_In_Hand = 0;
            }
        }

        //If bobaguest got hit drop BobaTeacups (NOT DONE)
        if (Boba_guests_got_hit == true)
        {
                for (int i = 0; i < Boba_In_Hand; i++) 
                {
                    closestTeacup.GetComponent<Rigidbody>().isKinematic = false;
                    Inhands = false;
                }
                ful_Hands = true;
        }

        //Picks up boba
        if (Inhands == true)
        {
            closestTeacup.transform.position = Boba_guests_pick_up_bobatea.transform.position;
        }

        if(closestTeacup == null)
            return;
    
        //If boba guest dose not have boba in hands and not got hit by the player and if their exits boba in scene follow closest bobatea
        if(ful_Hands == false && Boba_guests_got_hit == false)
            {
            nav.destination = closestTeacup.transform.position;
            }

        //Boba guest ska hitta närmaste boba tea och ta upp den och sen dricka den under 6s och sen ska den toma boba tean popa up och 
        //droppas på marken och sen ska han försöka hitta nästa närmaste boba tea för att göra samma sak.
        
        //If bobaguest has boba in hands and have drinked it (takes 6s) it goas out.
        if(ful_Hands == true)
        {
            timer_guestGoAroundRandom = 0;
            

            if (timer_guestGoAroundRandom >= 6)
                {
                nav.destination = Vector3.MoveTowards(transform.position, toTheExit.position, speed * Time.deltaTime);
                }
        }
    }

    //calclates the closest Teacup
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
    }

    void OnDrawGizmos() 
    {
        if (Debug_Bool == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Next_pos);
        }
    }


    private void OnTriggerEnter(Collider other) 
    {
        //If guest collides with boba and hands is not ful oick up boba
        if (other.gameObject.tag == "Boba" && ful_Hands == false)
        {
            Boba_In_Hand +=1;
            Inhands = true;
            
            //Get boba to stay in guests hand
            closestTeacup.GetComponent<Rigidbody>().isKinematic = true;
            
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
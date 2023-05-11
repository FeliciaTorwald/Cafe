using System.Collections;
using System.Collections.Generic;
using Carl.NewInteractionSystem;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class Boba_guests_follow_boba : MonoBehaviour
{
    //Gameobjects
    public GameObject[] allBobaTeacups;
    public GameObject closestTeacup;
    public GameObject prefab_FullTeacup;
    public GameObject prefab_EmptyTeacup;
    public GameObject Boba_guests_pick_up_bobatea;
    public GameObject UI_Text;

    //bools
    [SerializeField] private bool Debug_Bool;
    bool spawnOneDirtybobaTea = false;
    bool boba_guests_got_hit;
    bool boba_guest_is_hunting = false;
    public bool ful_Hands = false;
    bool intriggerarea;
    bool nollställ_huntBobatea = false;
    bool StartDrinking_bobatea = false;

    //transforms
    public Transform toTheExit;

    //refrences
    hit_boba_eating_guest boba_eating_guest_got_hit_true;
    NavMeshAgent nav;

    //floats
    [SerializeField] private float Radius = 20;
    public float Boba_In_Hand = 0;
    float speed = 500f;
    float counter;

        //timer
        float timer_guestGoAroundRandom;
        float guestDrinks_Timer;
        float timer_before_hunt_of_boba;

    //Audio
    SoundManager soundManager;
    public AudioSource source;
    public AudioClip hit;

    //Vector3
    Vector3 Next_pos;
    Vector3 VectorExit;

    void Start()
    {
        boba_eating_guest_got_hit_true = FindFirstObjectByType<hit_boba_eating_guest>();

        Boba_In_Hand = 0;
        nav = GetComponent<NavMeshAgent>();
        Next_pos = transform.position;

        InvokeRepeating(nameof(closestTeacups), 1,1);

        //generate first random position and walk to it
        Next_pos = Generate_Random_Pos.R_Pos(transform.position,Radius);
        nav.SetDestination(Next_pos);
        timer_guestGoAroundRandom = 0;

        soundManager = FindFirstObjectByType<SoundManager>();
    }
    private void Update()
    {
        timer_guestGoAroundRandom += Time.deltaTime;

        //timer for when boba guest drinks tea
        guestDrinks_Timer += Time.deltaTime;

        //timer before guest starts looking after next bobatea
        timer_before_hunt_of_boba += Time.deltaTime;

        //Looks if their is BobaTeacups in scene and count them
        allBobaTeacups = GameObject.FindGameObjectsWithTag("Boba");
        for (int i = 0; i < allBobaTeacups.Length; i++)
        {
            if (allBobaTeacups[i] != null)
            {
                counter++;
            }
        }

        //If no BobaTeacups is in scene go to an random Location
        if (counter == 0)
        {
            Random();
        }
        
        //Looks if boba guest got hit and if it has been hit it makes a sound
        if ((Input.GetKeyDown(KeyCode.Space)))
        {
            if (intriggerarea == true && ful_Hands == true)
            {
                source.PlayOneShot(hit);
                ful_Hands = false;
                closestTeacup.GetComponent<Rigidbody>().isKinematic = false;
                StartDrinking_bobatea = false;
            }
        } 
        if (Input.GetKeyDown(KeyCode.E))
        {
             if (intriggerarea == true && ful_Hands == true)
            {
                source.PlayOneShot(hit);
                ful_Hands = false;
                //closestTeacup.GetComponent<Rigidbody>().isKinematic = false;
                StartDrinking_bobatea = false;
            }
        }
        

        if(closestTeacup == null)
        {
            return;
        }
        
        //If boba guest dose not have boba in hands and not got hit by the player and if their exits boba in scene follow closest bobatea
        if(ful_Hands == false && allBobaTeacups.Length >= 1)
            {
                if (nollställ_huntBobatea == false)
                {
                timer_before_hunt_of_boba = 0;
                nollställ_huntBobatea = true;
                }

                if (timer_before_hunt_of_boba <= 10)
                {
                Random();
                }
                else
                {
                boba_guest_is_hunting = true;
                nav.destination = closestTeacup.transform.position;
                }
            }

        //Boba guest ska hitta närmaste boba tea och ta upp den och sen dricka den under 6s och sen ska den toma boba tean popa up och 
        //droppas på marken och sen ska han försöka hitta nästa närmaste boba tea för att göra samma sak.
        
        //If bobaguest has boba in hands and have drinked it (takes 6s) it goas out.
        
        if(ful_Hands == true)
        {
            
            //HÄR SKA VI HA EN IFSATS SOM KOLLAR OM PLAYER HÅLLER I BOBATEET OCH ISÅFALL SLUTAR HÅLLA I BOBATEET

            //Picks up boba
            closestTeacup.transform.position = Boba_guests_pick_up_bobatea.transform.position;
            nollställ_huntBobatea = false;

            //Buggen med att teakoppen blir ett ufo runt spelaren när space klickas och man står när bobaätaren är troligen för att
            //När playern håller i bobateet och sen att boba ätaren tar den från playern så tror fortfarande playern att den håller 
            //i boba teet och därför när vi klickar på space så droppas teet men eftersom 
            //spelaren fortfarande håller i det så rör det sig runt med spelaren
            // så vi behöver göra så när bobaätaren snor boba från playern så förstår även playern att den inte har bobatea i handen.

            //Fix so boba guest can´t pick up more bobatea
            boba_guest_is_hunting = false;

            //Get boba to stay in guests hand
            closestTeacup.GetComponent<Rigidbody>().isKinematic = true;

            if (StartDrinking_bobatea == false)
            {
                guestDrinks_Timer = 0;
                StartDrinking_bobatea = true;
                Debug.Log("SLurp");
                soundManager.Slurp();
            }
            
            //Boaba guest starts drinking tea and drinks it up after 6s
            if (guestDrinks_Timer >= 6)
            {
                if (closestTeacup.GetComponent<NewAbstractInteractable>().isHeld)
                {
                    closestTeacup.GetComponent<Interactable_NewFullTea>().playerInteractRef.NoLongerHoldingSomething();
                    closestTeacup.GetComponent<Interactable_NewFullTea>().playerInteractRef.interactables.Remove(closestTeacup.GetComponent<NewAbstractInteractable>());
                }
                spawnOneDirtybobaTea = true;
            }

            if(spawnOneDirtybobaTea == true)
            {
            Destroy(closestTeacup);
            Instantiate(prefab_EmptyTeacup, closestTeacup.transform.position, Quaternion.identity);
            ful_Hands = false;
            StartDrinking_bobatea = false;
            Boba_In_Hand = 0;
            spawnOneDirtybobaTea = false;
            }
        }
            //Walk to random position
            Random();
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

    void OnDrawGizmos() 
    {
        if (Debug_Bool == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Next_pos);
        }
    }

    //walk random function
    void Random()
    {
            if (Next_pos == Vector3.zero)
            {
                Next_pos = Generate_Random_Pos.R_Pos(transform.position,Radius);
                nav.SetDestination(Next_pos);
            }
            if (timer_guestGoAroundRandom >= 5 && Vector3.Distance(Next_pos, transform.position) >= 2f)
            {
                timer_guestGoAroundRandom = 0;
                Next_pos = Generate_Random_Pos.R_Pos(transform.position,Radius);
                nav.SetDestination(Next_pos);
            }
            if(Vector3.Distance(Next_pos, transform.position) <= 3f)
            {
                Next_pos = Generate_Random_Pos.R_Pos(transform.position,Radius);
                nav.SetDestination(Next_pos);
            }
    }
    private void OnTriggerEnter(Collider other) 
    {
        //If guest collides with boba and hands is not ful pick up boba
        if (other.gameObject.tag == "Boba" && ful_Hands == false && boba_guest_is_hunting == true)
        {
            ful_Hands = true;
        }

        if (other.gameObject.tag == "Player")
        {
            intriggerarea = true;
            UI_Text.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            intriggerarea = false;
            UI_Text.SetActive(false);
        }
    }

    
}
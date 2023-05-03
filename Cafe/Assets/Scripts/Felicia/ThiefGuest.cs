using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiefGuest : MonoBehaviour
{
    public string tagToDetect = "BobaPearls";

    public GameObject[] allBobaPearls;
    public GameObject closestBoba;
    public GameObject pickedUpBoba;

    private NavMeshAgent nav;

    public Transform hand;
    public Transform toTheExit;

    public bool pickedUp;

    bool intriggerarea;
    void Start()
    {
        allBobaPearls = GameObject.FindGameObjectsWithTag(tagToDetect);
        nav = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(closestBobas), 1, 1);
    }

    void Update()
    {
        if (closestBoba == null)
            return;

   

            nav.destination = closestBoba.transform.position;


        //if (hand != null)
        //{
        //    nav.destination = toTheExit.position;
        //}
    }

    public GameObject FindClosestBoba()
    {
        //List over boba pearls
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
        closestBoba = FindClosestBoba();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            intriggerarea = true;
        }

        if (other.gameObject.tag == "BobaPearls")
        {
            pickedUp = true;
            pickedUpBoba = closestBoba;
            pickedUpBoba.GetComponent<Rigidbody>().isKinematic = true;
            pickedUpBoba.transform.position = hand.transform.position;
            pickedUpBoba.transform.SetParent(hand);
            pickedUpBoba.GetComponent<BobaMovement>().enabled = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            intriggerarea = false;
        }

        if (other.gameObject.tag == "BobaPearls")
        {
            pickedUp = false;
        }

    }
}

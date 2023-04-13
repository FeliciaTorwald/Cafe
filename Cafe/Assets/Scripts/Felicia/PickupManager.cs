using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    List<Pickupable> pickupables;
   
    void Start()
    {
       pickupables= new List<Pickupable>();
    }

   
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Pickupable closest = pickupables.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).First();
            closest.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Pickupable>() != null)
        {
            pickupables.Add(other.GetComponent<Pickupable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Pickupable>() != null)
        {
            pickupables.Remove(other.GetComponent<Pickupable>());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

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
            //CleanList();
            Pickupable closest = pickupables.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).First();
            closest.Interact();
        }
    }
    private void CleanList()
    {
        for (int i = pickupables.Count - 1; i >= 0; i--)
        {
            if (pickupables[i] == null)
            {
                pickupables.RemoveAt(i);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Pickupable>() != null)
        {
            Debug.Log("Added pearl to list");
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

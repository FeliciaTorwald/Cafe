using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashDishes : MonoBehaviour
{
    // Kunna plocka up disken, DONE
    // när vi håller smutsig disk och klickar på e och är nära pond, mug försvinner 
    // mug spawnas vid ponden några sekunder senare och sen skjuts åt ett random håll med lerp
    bool inTriggerArea = false;
    bool holdDirtyBobatea = false;

    public bool startcleaningDirtyBobatea = false;

   
    public GameObject preFab;

    //EquipTool eT;

    private void Start()
    {
        //eT = FindFirstObjectByType<EquipTool>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inTriggerArea == true)
            {
                startcleaningDirtyBobatea = true;
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Washzone"))
        {
            inTriggerArea = true;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
            
        if (other.CompareTag("Washzone"))
        {
            inTriggerArea = false;
        }
    }
}

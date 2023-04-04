using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_water_In_Teapot : MonoBehaviour
{
    public GameObject water_In_Teapot_of_on;
    bool is_water_In_Teapot;
    BrewingInventory brewPot;
    void Start()
    {
        brewPot = FindFirstObjectByType<BrewingInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Pond")
        {
            if (Input.GetKey(KeyCode.E))
            {
                water_In_Teapot_of_on.SetActive(true);
                brewPot.hasWater = true;
            }
        }
    }
}

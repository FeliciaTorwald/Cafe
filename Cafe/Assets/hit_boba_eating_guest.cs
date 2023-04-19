using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_boba_eating_guest : MonoBehaviour
{
    bool intriggerarea = false;
    public bool Boba_guests_got_hit = false;

    void Start()
    { 
        
    }

    void Update()
    //If you are close to boba eating quest and press space print "slå boba gäst"
    {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (intriggerarea == true)
                {
                    Debug.Log("slå boba ätar gästen!");
                    Boba_guests_got_hit = true;
                }
            }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "boba eating guests")
        {
            intriggerarea = true;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "boba eating guests")
        {
            intriggerarea = false;
        }
    }
}

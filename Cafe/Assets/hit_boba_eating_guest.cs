using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_boba_eating_guest : MonoBehaviour
{
    bool intriggerarea = false;
    public bool Boba_guests_got_hit = false;
    bool holdMop;
    Boba_guests_follow_boba hBEG;

    void Start()
    {
        hBEG = FindFirstObjectByType<Boba_guests_follow_boba>();
    }

    void Update()
    //If you are close to boba eating quest and press space print "slå boba gäst"
    {
       
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (intriggerarea == true && holdMop)
                {
                    Debug.Log("slå boba ätar gästen!");
                    Boba_guests_got_hit = true;
                    //hBEG.Boba_In_Mouth = 0;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
        {
            hBEG.Boba_In_Mouth = 0;
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "boba eating guests")
        {
            intriggerarea = true;
        }

        if(other.gameObject.tag == "Mop")
        {
            holdMop = true;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "boba eating guests")
        {
            intriggerarea = false;
        }

        if (other.gameObject.tag == "Mop")
        {
            holdMop = false;
        }
    }
}

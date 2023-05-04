using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class hit_boba_eating_guest : MonoBehaviour
{
    public bool intriggerarea;
    public bool Boba_guests_got_hit = false;
    public bool holdMop;
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
            if (intriggerarea == true)
            {
                Boba_guests_got_hit = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (intriggerarea == true)
            {
                hBEG.Boba_In_Hand = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            intriggerarea = true;
        }

        if (other.gameObject.tag == "Mop")
        {
            holdMop = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            intriggerarea = false;
        }

        if (other.gameObject.tag == "Mop")
        {
            holdMop = false;
        }
    }
}

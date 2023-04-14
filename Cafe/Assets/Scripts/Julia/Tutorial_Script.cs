using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Script : MonoBehaviour
{
    public GameObject ShowControllsDone;
    bool DoNotShowControllsbool = true;

    void Update()
    {   
       if (Input.GetKeyDown(KeyCode.Escape) && DoNotShowControllsbool == false)
        {
            ShowControllsDone.SetActive(true);
            DoNotShowControllsbool = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && DoNotShowControllsbool == true)
        {
            ShowControllsDone.SetActive(false);
            DoNotShowControllsbool = false;
        }
    }
}

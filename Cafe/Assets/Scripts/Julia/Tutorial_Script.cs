using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Script : MonoBehaviour
{
    public GameObject ShowControllsDone;
    bool Escapepushed = false;

    void Update()
    {
        if (Escapepushed == false)
        {
           if (Input.GetKeyDown(KeyCode.Escape))
        {
            Invoke("DoNotShowControlls", 0);
     
        }
        
        }

        if (Escapepushed == true)
        {
           if (Input.GetKeyDown(KeyCode.Escape))
        {
            Invoke("DoShowControlls", 0);
           
        } 
        }

        //When player tested all controlls stop showing controlls after ... seconds
    }
        private void DoNotShowControlls()
        {
            ShowControllsDone.SetActive(false);
            
        }
         private void DoShowControlls()
        {
            ShowControllsDone.SetActive(true);
        }
}

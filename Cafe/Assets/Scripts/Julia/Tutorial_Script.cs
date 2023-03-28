using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Script : MonoBehaviour
{
     bool W_is_pressed = false;
    bool  A_is_pressed = false;
    bool S_is_pressed = false;
    bool D_is_pressed = false;
    public GameObject ShowControllsDone;
    public float time = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            W_is_pressed = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            A_is_pressed = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            S_is_pressed = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {

            D_is_pressed = true;
        }

        //When player tested all controlls stop showing controlls after ... seconds
        if (W_is_pressed == true && A_is_pressed == true && S_is_pressed == true && D_is_pressed == true)
        {
            Invoke("DoNotShowControlls", 3);  
        }
    }
        private void DoNotShowControlls()
        {
            ShowControllsDone.SetActive(false);
        }
}

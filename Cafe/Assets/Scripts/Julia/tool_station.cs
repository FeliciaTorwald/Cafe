using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tool_station : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.tag == "Brush")
        {
            Debug.Log("Net2");
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Net");
            }
        }

        if(other.gameObject.tag == "Player" && other.gameObject.tag == "Brush")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Brush");
            }
        }

        if(other.gameObject.tag == "Player" && other.gameObject.tag == "Teapot")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Teapot");
            }
        }
    }

}

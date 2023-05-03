using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mop")
        {
            gameObject.SetActive(false);//if no objectpooling is used turn to destroy instead

        }

    }
}

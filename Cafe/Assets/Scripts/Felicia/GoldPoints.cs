using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           Destroy(gameObject);

            FindObjectOfType<BobaCounter>().AddBoba(1);

            
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    public GameObject preFabGold;
    public bool onOrderFulfilled = true;


    private void Spawn()
    {
       if(onOrderFulfilled)
        {
        Instantiate(preFabGold, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("coillding");
        if (other.tag == ("Boba"))
        {
            Spawn();
        }

    }
}

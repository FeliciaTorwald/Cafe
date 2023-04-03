using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    public GameObject preFabGold;
    public bool onOrderFulfilled = true;
     GameObject coin; 


    //TO DO turn off toolscript on the tea so you cant get unlimited money glitch
    private void Spawn()
    {
        if (onOrderFulfilled)
        {
            GameObject coin = Instantiate(preFabGold, transform.position, Quaternion.identity);
        }

    }

    public void Destroy()
    {
        Destroy(coin);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Boba"))
        {
            Spawn();
        }

    }
}

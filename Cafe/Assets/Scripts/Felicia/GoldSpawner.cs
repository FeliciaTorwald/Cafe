using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    public GameObject preFabGold;
    public bool onOrderFullfilled = true;
    GameObject coin; 


    
    public void Spawn()
    {
        if (onOrderFullfilled)
        {
            GameObject coin = Instantiate(preFabGold, transform.position, Quaternion.identity);
        }

    }

    public void Destroy()
    {
        Destroy(coin,0.5f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Boba"))
        {
            Spawn();
        }

    }
}

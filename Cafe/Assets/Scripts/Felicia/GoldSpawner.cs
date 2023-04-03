using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    public GameObject preFabGold;
    public bool onOrderFulfilled = true;
  

    //TO DO turn off toolscript on the tea so you cant get unlimited money glitch
    private void Spawn()
    {
       if(onOrderFulfilled)
        {
        Instantiate(preFabGold, transform.position, Quaternion.identity);
        }
    }

  

    
private void OnTriggerExit(Collider other)
    {
        Debug.Log("coilldingGold");
        if (other.tag == ("Boba"))
        {
            Spawn();
        }

    }
}

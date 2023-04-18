using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {   
        if (other.tag == ("Player"))
        {
           //FindObjectOfType<GoldSpawner>().DestroyCoin();
           Destroy(gameObject);
           FindObjectOfType<GoldCounter>().AddGold(10);
        }
    }
    
}

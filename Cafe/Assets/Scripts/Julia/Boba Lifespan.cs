using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaLifespan : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Destroy_boba",20);
    }

    private void Destroy_boba()
    {
        Destroy(gameObject);
    }
}

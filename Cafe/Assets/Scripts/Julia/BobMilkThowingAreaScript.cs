using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobMilkThowingAreaScript : MonoBehaviour
{
    WashDishes washDishesRef;
    
    void Start()
    {
        washDishesRef = FindFirstObjectByType<WashDishes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (washDishesRef.startcleaningDirtyBobatea == true)
        {
           float xpos = Random.Range(0,10);
           float ypos = Random.Range(0,10);
           float zpos = Random.Range(0,10);

           Vector3 Spawnpos = new Vector3(xpos, ypos, zpos);
        }
        
    }
}

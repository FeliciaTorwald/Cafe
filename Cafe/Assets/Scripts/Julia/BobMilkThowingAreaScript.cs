using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobMilkThowingAreaScript : MonoBehaviour
{
    WashDishes washDishesRef;
    public GameObject preFab;
    
    void Start()
    {
        washDishesRef = FindFirstObjectByType<WashDishes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (washDishesRef.startcleaningDirtyBobatea == true)
        {
           float xpos = Random.Range(10, 26);
           float ypos = Random.Range(10, 10);
            float zpos = Random.Range(-14, 17);

           Vector3 Spawnpos = new Vector3(xpos, ypos, zpos);
           Instantiate(preFab, Spawnpos,transform.rotation);
           washDishesRef.startcleaningDirtyBobatea = false;
        }
        
    }
}

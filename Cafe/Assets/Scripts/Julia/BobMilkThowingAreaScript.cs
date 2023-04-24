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
            // Get the center of the circle collider
            Vector3 center = circleCollider.transform.position;

            // Get a random point within the circle collider
            float distance = Random.Range(0f, circleCollider.radius);
            float angle = Random.Range(0f, 360f);
            Vector3 randomPoint = center + new Vector3(distance * Mathf.Cos(angle * Mathf.Deg2Rad), 0f, distance * Mathf.Sin(angle * Mathf.Deg2Rad));
        }
        
    }
}

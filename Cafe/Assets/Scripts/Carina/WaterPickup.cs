using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickup : MonoBehaviour
{
    public bool hasWater;

    [SerializeField] GameObject waterInBucket;

    Resource addWater;

    private void Start()
    {
        addWater = FindObjectOfType<Resource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BrewingPot"))
        {
            if (hasWater)
            {
                addWater.Gather();
                hasWater = false;
                waterInBucket.gameObject.SetActive(false);
            }

        }

        if (other.gameObject.CompareTag("Pond"))
        {
            waterInBucket.gameObject.SetActive(true);
            hasWater = true;
        }
    }
}

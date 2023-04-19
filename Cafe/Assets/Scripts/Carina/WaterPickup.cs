using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickup : MonoBehaviour
{
    public bool hasWater;
    public bool BPTriggerArea;
    public bool PTriggerArea;

    [SerializeField] GameObject waterInBucket;

    CraftingTea canMakeTeaCheck;
    Resource addWater;
    EquipTool eQ;

    private void Start()
    {
        addWater = FindObjectOfType<Resource>();
        eQ = FindObjectOfType<EquipTool>();
        canMakeTeaCheck = FindObjectOfType<CraftingTea>();
    }

    //private void Update()
    //{
    //    AddWaterToBucket();
    //    AddWaterToKettle(); 
    //}
    public void AddWaterToKettle()
    {
        //if (BPTriggerArea)
        //{
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hasWater)
            {
                addWater.Gather();
                hasWater = false;
                waterInBucket.gameObject.SetActive(false);
                canMakeTeaCheck.UpdateCanCraft();
            }

            }
        //}
    }
    public void AddWaterToBucket()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
        //    if (PTriggerArea)
        //{
            waterInBucket.gameObject.SetActive(true);
            hasWater = true;
        //}

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BrewingPot"))
        {
            BPTriggerArea = true;
            //if (hasWater)
            //{
            //    addWater.Gather();
            //    hasWater = false;
            //    waterInBucket.gameObject.SetActive(false);
            //}

        }

        if (other.gameObject.CompareTag("Pond"))
        {
            PTriggerArea = true;
            //waterInBucket.gameObject.SetActive(true);
            //hasWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BrewingPot"))
        {
            BPTriggerArea = false;
        }

        if (other.gameObject.CompareTag("Pond"))
        {
            PTriggerArea = false;
        }


    }
}

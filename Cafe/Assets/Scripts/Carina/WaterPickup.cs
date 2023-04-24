using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickup : MonoBehaviour
{
    public bool hasWater;
    public bool BPTriggerArea;
    public bool PTriggerArea;

    [SerializeField] GameObject waterInBucket;

    CraftingUI canMakeTeaCheck;
    Resource addWater;
    EquipTool eQ;

    private void Start()
    {
        addWater = FindObjectOfType<Resource>();
        eQ = FindObjectOfType<EquipTool>();
        canMakeTeaCheck = FindObjectOfType<CraftingUI>();
    }
    public void AddWaterToKettle()
    {
        if (!HoldingBucket()) { return; }
        if (!Input.GetKeyDown(KeyCode.E)) { return; }
        if (!hasWater) { return; }
        addWater.Gather();
        hasWater = false;
        waterInBucket.gameObject.SetActive(false);
        canMakeTeaCheck.UpdateCanCraft();

    }

    private bool HoldingBucket()
    {
        EquipTool[] tools = FindObjectsOfType<EquipTool>();
        foreach (EquipTool tool in tools)
        {
            if (tool.IdentifyToolType() == ToolType.EmptyBucket && tool.equipped)
            {
                return true;
            }
        }
        return false;
    }
    public void AddWaterToBucket()
    {
        if (HoldingBucket())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                waterInBucket.gameObject.SetActive(true);
                hasWater = true;
            }
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

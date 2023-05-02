using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickup : MonoBehaviour
{
    public bool hasWater;
    public bool BPTriggerArea;
    public bool PTriggerArea;

    [SerializeField] GameObject waterInBucket;
    [SerializeField] GameObject waterInKettle;

    CraftingUI canMakeTeaCheck;
    Resource addWater;
    EquipTool eQ;

    private void Start()
    {
        addWater = FindObjectOfType<Resource>();
        eQ = FindObjectOfType<EquipTool>();
        canMakeTeaCheck = FindObjectOfType<CraftingUI>();
    }

    private void Update()
    {
        if (hasWater)
        {
            RemoveWater();
        }
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
        if (!waterInKettle.activeInHierarchy)
        {
            waterInKettle.gameObject.SetActive(true);
        }
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

    public void RemoveWater()
    {
        var x = transform.rotation.eulerAngles.x;
        var z = transform.rotation.eulerAngles.z;

        if (x <= 91 && x >= 89 || x <= 271 && x >= 268 || z <= 91 && z >= 89 || z <= 271 && z >= 268)
        {
            hasWater = false;
            waterInBucket.gameObject.SetActive(false);
        }
    }
}

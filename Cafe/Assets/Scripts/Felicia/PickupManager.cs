using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;

public class PickupManager : MonoBehaviour
{
    public List<Pickupable> pickupables;
    [SerializeField] private TMP_Text pickUpDisplay;
    [SerializeField] private Vector3 offset;
    public Pickupable heldToolRef;
    Camera mainCameraRef;
    private Player playerScriptRef;

    void Start()
    {
        pickupables = new List<Pickupable>();
        Invoke(nameof(UpdatePlayerPickupDisplay), 0.5f);
        UpdatePlayerPickupDisplay();
        mainCameraRef = Camera.main;
        playerScriptRef = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldToolRef != null && heldToolRef.toolType is ToolType.Tea && playerScriptRef.interactables.Count == 0)
                UpdateClosestPickupable()?.Interact();
            else if (heldToolRef != null && heldToolRef.toolType is ToolType.Tea)
                playerScriptRef.ServeTea();
            else
                UpdateClosestPickupable()?.Interact();
        }
    }

    private void LateUpdate()
    {
        pickUpDisplay.rectTransform.position = mainCameraRef.WorldToScreenPoint(transform.position) + offset;
    }

    private void UpdatePlayerPickupDisplay()
    {
        Pickupable pickupable = UpdateClosestPickupable();
        if (heldToolRef != null)
        {
            if (heldToolRef.toolType == ToolType.EmptyBucket)
            {
                if (heldToolRef.GetComponent<WaterPickup>().PTriggerArea)
                {
                    pickUpDisplay.SetText("E: Draw water");
                    Invoke(nameof(UpdatePlayerPickupDisplay), 0.5f);
                    return;
                }
                if (heldToolRef.GetComponent<WaterPickup>().BPTriggerArea && heldToolRef.GetComponent<WaterPickup>().hasWater)
                {
                    pickUpDisplay.SetText("E: Add water to pot");
                    Invoke(nameof(UpdatePlayerPickupDisplay), 0.5f);
                    return;
                }
            }
            
            if (heldToolRef.toolType is ToolType.Boba)
            {
                pickUpDisplay.SetText("Space: Throw boba pearl");
                Invoke(nameof(UpdatePlayerPickupDisplay), 0.5f);
                return;
            }

            if (heldToolRef.toolType is ToolType.Tea)
            {
                //Add code to check if tea is servable
            }

            if (heldToolRef.toolType is ToolType.EmptyTea)
            {
                //Add code to add dish to water
            }
        }
        
        if (heldToolRef != null)
        {
            pickUpDisplay.SetText("E: Drop");
            Invoke(nameof(UpdatePlayerPickupDisplay), 0.5f);
            return;
        }
            
        
        if (pickupable != null)
        {
            string toolType = UpdateClosestPickupable().IdentifyToolType().ToString();
            pickUpDisplay.SetText("E: Pick up: " + toolType);
        }
        else
        {
            pickUpDisplay.SetText("");
        }
        
        // if (pickupable != null)
        // {
        //     if (pickupable.GetComponent<EquipTool>() != null)
        //     {
        //         string toolType = UpdateClosestPickupable().GetComponent<EquipTool>().IdentifyToolType().ToString();
        //         pickUpDisplay.SetText("E: Pick up: " + toolType);
        //         pickupable = null;
        //     }
        //     else if (pickupable.GetComponent<BobaBall>() != null)
        //     {
        //         string toolType = UpdateClosestPickupable().GetComponent<BobaBall>().IdentifyToolType().ToString();
        //         pickUpDisplay.SetText("E: Pick up: " + toolType);
        //         pickupable = null;
        //     }
        // }
        // else
        // {
        //     pickUpDisplay.SetText("");
        // }
        
        Invoke(nameof(UpdatePlayerPickupDisplay), 0.5f);
    }
    
    private Pickupable UpdateClosestPickupable()
    {
        CleanList();
        if (pickupables.Count == 0)
        {
            return null;
        }
        Pickupable closest = pickupables.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).First();
            return closest;
        
    }
    
    private void CleanList()
    {
        for (int i = pickupables.Count - 1; i >= 0; i--)
        {
            if (pickupables[i] == null)
            {
                pickupables.RemoveAt(i);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Pickupable>() != null)
        {
            Debug.Log("Added item to list");
            pickupables.Add(other.GetComponent<Pickupable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Pickupable>() != null)
        {
            pickupables.Remove(other.GetComponent<Pickupable>());
        }
    }

    public void HoldingSomething(Pickupable pickupable)
    {
        heldToolRef = pickupable;
    }

    public void NoLongerHoldingSomething()
    {
        heldToolRef = null;
    }
}

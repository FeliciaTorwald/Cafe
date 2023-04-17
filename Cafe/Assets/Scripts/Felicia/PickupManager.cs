using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;

public class PickupManager : MonoBehaviour
{
    List<Pickupable> pickupables;
    [SerializeField] private Canvas interactionCanvas;
    [SerializeField] private TMP_Text pickUpDisplay;


    void Start()
    {
       pickupables= new List<Pickupable>();
       Invoke(nameof(UpdatePlayerPickupDisplay), 0.5f);
       UpdatePlayerPickupDisplay();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateClosestPickupable().Interact();
        }
        
       RotateCanvas();
    }

    private void RotateCanvas()
    {
        interactionCanvas.transform.forward = Camera.main.transform.forward;
    }

    private void UpdatePlayerPickupDisplay()
    {
        Pickupable pickupable = UpdateClosestPickupable();  
        if (pickupable != null)
        {
            if (pickupable.GetComponent<EquipTool>() != null)
            {
                string toolType = UpdateClosestPickupable().GetComponent<EquipTool>().IdentifyToolType().ToString();
                pickUpDisplay.SetText("E: Pick up: " + toolType);
                pickupable = null;
            }
            else if (pickupable.GetComponent<BobaBall>() != null)
            {
                string toolType = UpdateClosestPickupable().GetComponent<BobaBall>().IdentifyToolType().ToString();
                pickUpDisplay.SetText("E: Pick up: " + toolType);
                pickupable = null;
            }
        }
        else
        {
            pickUpDisplay.SetText("");
        }
        
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
            Debug.Log("Added pearl to list");
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
}

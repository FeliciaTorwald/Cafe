using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Carl.NewInteractionSystem;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class NewInteract : MonoBehaviour
{
    [SerializeField] private TMP_Text toolTipDisplay;
    [SerializeField] private Vector3 offset;
    
    public List<NewAbstractInteractable> interactables = new();
    private NewAbstractInteractable heldObjectRef;
    private Camera mainCameraRef;

    private void Start()
    {
        mainCameraRef = Camera.main;
        Invoke(nameof(UpdatePlayerToolTip), .5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            NewAbstractInteractable closest;
            
            if (interactables.Count != 0 && !heldObjectRef)
            {
                closest = FindClosestInteractable();
                if (closest)
                    closest.Interact(this);
            }
            else if (interactables.Count != 0 && heldObjectRef)
            {
                heldObjectRef.Interact(this);
            }
        }
    }

    private void LateUpdate()
    {
        toolTipDisplay.rectTransform.position = mainCameraRef.WorldToScreenPoint(transform.position) + offset;
    }

    private void UpdatePlayerToolTip()
    {
        if (heldObjectRef)
        {
            switch (heldObjectRef.newItemType)
            {
                case NewItemType.boba:
                    toolTipDisplay.SetText("E: Throw boba");
                    break;
                case NewItemType.fullBucket:
                    Interactable_NewBucket bucketScript = heldObjectRef.GetComponent<Interactable_NewBucket>();
                    if (bucketScript.hasWater)
                    {
                        if (bucketScript.closeToPot)
                        {
                            toolTipDisplay.SetText("E: Add water to pot");
                        }
                        else
                        {
                            toolTipDisplay.SetText("E: Drop");
                        }
                    }
                    else if (!bucketScript.hasWater)
                    {
                        if (bucketScript.closeToWater)
                        {
                            toolTipDisplay.SetText("E: Draw water");
                        }
                        else
                        {
                            toolTipDisplay.SetText("E: Drop");
                        }
                    }
                    break;
                case NewItemType.finishedTea:
                    Interactable_NewFullTea fullTeaScript = heldObjectRef.GetComponent<Interactable_NewFullTea>();
                    NewBobaTeaHandler closestTable = fullTeaScript.FindClosestTable();
                    if (closestTable)
                    {
                        if (closestTable.guestRef)
                        {
                            if (closestTable.guestRef.stateMachine.currentState is GuestStateID.Ordered)
                            {
                                toolTipDisplay.SetText("E: Serve tea");
                            }
                            else if (closestTable.guestRef.stateMachine.currentState is GuestStateID.AtTable)
                            {
                                toolTipDisplay.SetText("E: Take order");
                            }
                        }
                        else
                        {
                            toolTipDisplay.SetText("E: Drop");
                        }
                    }
                    break;
                case NewItemType.dirtyTea:
                    toolTipDisplay.SetText("E: Throw dirty dish");
                    break;
            }
        }
        else
        {
            NewAbstractInteractable closest = FindClosestInteractable();

            if (!closest)
            {
                toolTipDisplay.SetText("");
            }
            else
            {
                switch (closest.newItemType)
                {
                    case NewItemType.boba:
                        toolTipDisplay.SetText("E: Pick up boba pearl");
                        break;
                    case NewItemType.fullBucket:
                        Interactable_NewBucket bucketScript = closest.GetComponent<Interactable_NewBucket>();
                        if (bucketScript.hasWater)
                            toolTipDisplay.SetText("E: Pick up full bucket");
                        else if (!bucketScript.hasWater)
                            toolTipDisplay.SetText("E: Pick up empty bucket");
                        break;
                    case NewItemType.finishedTea:
                        toolTipDisplay.SetText("E: Pick up tea");
                        break;
                    case NewItemType.dirtyTea:
                        toolTipDisplay.SetText("E: Pick up dirty dishes");
                        break;
                    case NewItemType.bobaHandler:
                        NewBobaTeaHandler bobaHandlerRef = closest.GetComponent<NewBobaTeaHandler>();
                        if (bobaHandlerRef.guestRef.stateMachine.currentState is GuestStateID.AtTable)
                            toolTipDisplay.SetText("E: Take order");
                        else
                            toolTipDisplay.SetText("");
                        break;
                }
            }
            
        }
        
        Invoke(nameof(UpdatePlayerToolTip), .3f);
    }
    
    
    private NewAbstractInteractable FindClosestInteractable()
    {
        CleanList();
        if (interactables.Count == 0)
            return null;
        NewAbstractInteractable closest = interactables.OrderBy(x => 
                Vector3.Distance(transform.position, x.transform.position)).First();
        return closest;
    }

    private void CleanList()
    {
        for (int i = interactables.Count - 1; i >= 0; i--)
        {
            if (interactables[i] is null)
            {
                interactables.RemoveAt(i);
            }
            else if (interactables[i].newItemType is NewItemType.bobaHandler &&
                     !interactables[i].GetComponent<NewBobaTeaHandler>().guestRef)
            {
                interactables.RemoveAt(i);
            }
        }        
    }
    
    public void HoldingSomething(NewAbstractInteractable newInteractable)
    {
        heldObjectRef = newInteractable;
    }

    public void NoLongerHoldingSomething()
    {
        heldObjectRef = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NewAbstractInteractable>() != null)
        {
            interactables.Add(other.GetComponent<NewAbstractInteractable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NewAbstractInteractable>() != null)
        {
            interactables.Remove(other.GetComponent<NewAbstractInteractable>());
        }
    }
}
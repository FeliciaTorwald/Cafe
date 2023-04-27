using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestInteraction : MonoBehaviour
{
    public Guest parentGuest;
    public BobaTeaHandler table;
    public float irritation;
    public float mad;
    [SerializeField] private Slider angerMeter;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            ServeGuest(TeaType.TypeA);

        if (parentGuest.stateMachine.currentState == GuestStateID.AtTable && irritation < mad)
        {
            irritation += 1 * Time.deltaTime;
            UpdateAngerMeter();
        }
        else if (parentGuest.stateMachine.currentState == GuestStateID.AtTable && irritation >= mad)
        {
            //parentGuest.stateMachine.ChangeState(GuestAngry);
            //TODO: Implement angry state
            // irritation = 0f;
        }
        
        if (parentGuest.stateMachine.currentState == GuestStateID.Ordered && irritation < mad)
        {
            irritation += 1 * Time.deltaTime;
            UpdateAngerMeter();
        }
        else if (parentGuest.stateMachine.currentState == GuestStateID.Ordered && irritation >= mad)
        {
            //parentGuest.stateMachine.ChangeState(GuestAngry);
            //TODO: Implement angry state
            // irritation = 0f;
        }
    }

    public void ServeGuest(TeaType teaType)
    {
        //Add check for whether the correct tea is served or not
        if (teaType == parentGuest.teaType && parentGuest.stateMachine.currentState == GuestStateID.Ordered)
            parentGuest.stateMachine.ChangeState(GuestStateID.Served);
    }

    public void TakeOrder()
    {
        if (parentGuest.stateMachine.currentState == GuestStateID.AtTable)
            parentGuest.stateMachine.ChangeState(GuestStateID.Ordered);
    }

    public void ToggleAngerMeter(bool toggle)
    {
        angerMeter.gameObject.SetActive(toggle);
    }
    
    private void UpdateAngerMeter()
    {
        angerMeter.value = irritation;
    }
}
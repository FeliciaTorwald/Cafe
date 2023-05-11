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
    public float maxIrritationBeforeLeaving;
    [SerializeField] private Slider angerMeter;
    [SerializeField] private float angerShakeAmount;
    [SerializeField] private float angerShakeTime;
    [SerializeField] private Image sliderFill;
    [SerializeField] private Color minIrritationColor;
    [SerializeField] private Color maxIrritationColor;
    private Transform guestPos;
    [SerializeField] private Transform sliderRect;
    SoundManager soundManager;

    private void Start()
    {
        angerMeter.maxValue = maxIrritationBeforeLeaving;
        soundManager = FindObjectOfType<SoundManager>();
        guestPos = parentGuest.GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            ServeGuest(TeaType.TypeA);
        
        if (parentGuest.stateMachine.currentState is GuestStateID.AtTable or GuestStateID.Ordered)
        {
            if (irritation > maxIrritationBeforeLeaving*.88)
            {
                //Debug.Log("Shaking");
                AngerShake();
            }
        }
        
        if (parentGuest.stateMachine.currentState == GuestStateID.AtTable && irritation < maxIrritationBeforeLeaving)
        {
            irritation += 1 * Time.deltaTime;
            UpdateAngerMeter();
        }
        else if (parentGuest.stateMachine.currentState == GuestStateID.AtTable && irritation >= maxIrritationBeforeLeaving)
        {
            parentGuest.angry = true;
            parentGuest.stateMachine.ChangeState(GuestStateID.Angry);
            ToggleAngerMeter(false);
            soundManager.Angry();
        }
        
        if (parentGuest.stateMachine.currentState == GuestStateID.Ordered && irritation < maxIrritationBeforeLeaving)
        {
            irritation += 1 * Time.deltaTime;
            UpdateAngerMeter();
        }
        else if (parentGuest.stateMachine.currentState == GuestStateID.Ordered && irritation >= maxIrritationBeforeLeaving)
        {
            parentGuest.angry = true;
            parentGuest.stateMachine.ChangeState(GuestStateID.Angry);
            ToggleAngerMeter(false);
            soundManager.Angry();
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
        ChangeAngerMeterColor();
    }
    
    private void ChangeAngerMeterColor()
    {
        float fillAmount = angerMeter.value / angerMeter.maxValue;
        sliderFill.color = Color.Lerp(minIrritationColor, maxIrritationColor, fillAmount);
    }
    
    private void AngerShake()
    {
        Vector3 originalPosition = guestPos.position;
        Vector2 fillAreaOriginalPosition = sliderRect.position;
        
        float shakeX = Mathf.Sin(Time.time * angerShakeTime) * angerShakeAmount;
        float shakeZ = Mathf.Cos(Time.time * angerShakeTime) * angerShakeAmount;
        Vector3 offset = new Vector3(shakeX, 0f, shakeZ);
        Vector2 fillOffset = new Vector3(shakeX,shakeZ, 0f);
        guestPos.position = originalPosition + offset;
        sliderRect.localPosition = fillAreaOriginalPosition + fillOffset;
    }
}
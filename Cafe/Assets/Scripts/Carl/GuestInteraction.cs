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
    [SerializeField] private RectTransform guestCanvasRef;
    [SerializeField] private Slider angerMeter;
    [SerializeField] private Image sliderFill;
    [SerializeField] private Color minIrritationColor;
    [SerializeField] private Color maxIrritationColor;
    
    private void Start()
    {
        angerMeter.maxValue = maxIrritationBeforeLeaving;
        guestCanvasRef.GetComponent<Canvas>().worldCamera = Camera.main;
    SoundManager soundManager;

    private void Start()
    {
        angerMeter.maxValue = maxIrritationBeforeLeaving;
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            ServeGuest(TeaType.TypeA);

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
        // CheckOffScreenPosition();
    }


    private void ChangeAngerMeterColor()
    {
        float fillAmount = angerMeter.value / angerMeter.maxValue;
        sliderFill.color = Color.Lerp(minIrritationColor, maxIrritationColor, fillAmount);
    }
    
    private void CheckOffScreenPosition()
    {
        Transform parentObject = parentGuest.gameObject.transform;
        Vector3 localPos = parentObject.InverseTransformPoint(guestCanvasRef.position);
        Vector3 screenPos = Camera.main.WorldToViewportPoint(parentObject.TransformPoint(localPos));
        Vector2 anchorPos = guestCanvasRef.anchorMax;

        if (screenPos.x < 0f)
        {
            anchorPos.x = 0f;
        }
        else if (screenPos.x > 1f)
        {
            anchorPos.x = 1f;
        }

        if (screenPos.y < 0f)
        {
            anchorPos.y = 0f;
        }
        else if (screenPos.y > 1f)
        {
            anchorPos.y = 1f;
        }

        guestCanvasRef.anchorMax = anchorPos;
        guestCanvasRef.anchorMin = anchorPos;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class teaOrderTicket : MonoBehaviour
{
    public OrderImageUI orderingRef;
    public TeaType teaType;
    public Slider guestAngerMeter;
    public Guest guestRef;
    [SerializeField] private Image sliderFill;
    [SerializeField] private Color minIrritationColor;
    [SerializeField] private Color maxIrritationColor;
    
    private void Start()
    {
        guestAngerMeter.maxValue = guestRef.guestInteraction.maxIrritationBeforeLeaving;
        guestRef.orderTicketRef = this;
    }

    private void Update()
    {
        UpdateAngerMeter();
    }

    public void UpdateAngerMeter()
    {
        guestAngerMeter.value = guestRef.guestInteraction.irritation;
        UdpateMeterColor();
    }

    private void UdpateMeterColor()
    {
        float fillAmount = guestAngerMeter.value / guestAngerMeter.maxValue;
        sliderFill.color = Color.Lerp(minIrritationColor, maxIrritationColor, fillAmount);        
    }

    private void ShakeMeter()
    {
        
    }
    
    public void RemoveTicket()
    {
        guestRef.orderTicketRef = null;
        Destroy(gameObject);
    }
}
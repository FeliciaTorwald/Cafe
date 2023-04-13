using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;
    private Vector3 relataiveVerticalMovement;
    private Vector3 relativeHorizontalMovement;
    private Vector3 relativeForward;
    private Vector3 relativeHorizontal;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 600f;
    
    private void Start()
    {
        SetRelativeVector();
    }

    private void Update()
    {
        GetAndAlignInput();
        UpdateMovement();
    }

    private void SetRelativeVector()
    {
        relativeForward = Camera.main.transform.forward;
        relativeHorizontal = Camera.main.transform.right;
        relativeForward.y = 0f;
        relativeHorizontal.y = 0f;
        relativeForward = relativeForward.normalized;
        relativeHorizontal = relativeHorizontal.normalized;
    }
    
    private void GetAndAlignInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        relataiveVerticalMovement = verticalInput * relativeForward;
        relativeHorizontalMovement = horizontalInput * relativeHorizontal;
    }

    private void UpdateMovement()
    {
        Vector3 movement = relataiveVerticalMovement + relativeHorizontalMovement;
        transform.Translate(movement * (speed * Time. deltaTime), Space.World);
        
        if(movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,rotationSpeed * Time.deltaTime);
        }
    }
}

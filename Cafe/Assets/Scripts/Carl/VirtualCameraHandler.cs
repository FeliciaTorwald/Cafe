using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VirtualCameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private BoxCollider cameraChangeTrigger; 
    [SerializeField] private bool isTopCamera;
    [SerializeField] private float idleTimer = 5f;
    private CameraController cameraController;

    public bool topCameraActive;
    
    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        cameraController.RegisterCamera(virtualCamera);
        
    }

    private void Update()
    {
        if (isTopCamera)
        {
            if (!Input.anyKey && idleTimer <= 0f && !topCameraActive)
            {
                cameraController.SwitchCamera(virtualCamera, isTopCamera);
                idleTimer = 5f;
                topCameraActive = true;
            }
            else if (!Input.anyKey && !topCameraActive)
                idleTimer -= 1 * Time.deltaTime;
            else if (Input.anyKey && !topCameraActive)
                idleTimer = 5f;
            else if (Input.anyKey && topCameraActive)
            {
                cameraController.SwitchCamera(virtualCamera, isTopCamera);
                topCameraActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            cameraController.SwitchCamera(virtualCamera, isTopCamera);
    }
}
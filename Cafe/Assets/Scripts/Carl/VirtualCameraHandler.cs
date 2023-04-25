using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VirtualCameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private BoxCollider cameraChangeTrigger; 
    [SerializeField] private bool isZoomedOutCamera;
    [SerializeField] private float idleTimer = 5f;
    private CameraController cameraController;

    public bool zoomOutCameraActive;
    
    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        cameraController.RegisterCamera(virtualCamera);

        virtualCamera.Follow = FindObjectOfType<NewPlayerMovement>().transform;
    }

    private void Update()
    {
        if (isZoomedOutCamera)
        {
            if (!Input.anyKey && idleTimer <= 0f && !zoomOutCameraActive)
            {
                cameraController.SwitchCamera(virtualCamera, isZoomedOutCamera);
                idleTimer = 5f;
                zoomOutCameraActive = true;
            }
            else if (!Input.anyKey && !zoomOutCameraActive)
                idleTimer -= 1 * Time.deltaTime;
            else if (Input.anyKey && !zoomOutCameraActive)
                idleTimer = 5f;
            else if (Input.anyKey && zoomOutCameraActive)
            {
                cameraController.SwitchCamera(virtualCamera, isZoomedOutCamera);
                zoomOutCameraActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            cameraController.SwitchCamera(virtualCamera, isZoomedOutCamera);
    }
}
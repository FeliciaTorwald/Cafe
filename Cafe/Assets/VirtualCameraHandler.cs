using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VirtualCameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private BoxCollider cameraChangeTrigger; 
    
    private void Start()
    { 
        CameraController.RegisterCamera(virtualCamera);
    }

    private void OnTriggerEnter(Collider other)
    {
        CameraController.SwitchCamera(virtualCamera);
    }
}
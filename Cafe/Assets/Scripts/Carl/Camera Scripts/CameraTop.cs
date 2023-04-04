using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraTop : MonoBehaviour
{
    [SerializeField] private SceneCamera cameraController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            cameraController.MoveCamera(gameObject.transform.position);
    }
}

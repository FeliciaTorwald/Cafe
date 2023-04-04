using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBottomMid : MonoBehaviour
{
    [SerializeField] private SceneCamera cameraController;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            cameraController.MoveCamera(gameObject.transform.position);
    }
}

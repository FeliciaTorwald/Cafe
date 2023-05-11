using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Look_At_Camera : MonoBehaviour
{
void LateUpdate() 
{
    transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
}
}

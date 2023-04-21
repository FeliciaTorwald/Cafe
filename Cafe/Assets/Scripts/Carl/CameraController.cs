using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    public static CinemachineVirtualCamera activeCamera = null;

    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == activeCamera;
    }
    
    public void SwitchCamera(CinemachineVirtualCamera camera, bool topCameraSwitch)
    {
        if (!topCameraSwitch)
        {
            camera.Priority = 10;
            activeCamera = camera;

            foreach (CinemachineVirtualCamera c in cameras)
            {
                if (c != activeCamera && c.Priority != 0)
                {
                    c.Priority = 0;
                }
            }
        }
        else
        {
            if (camera.Priority == 10)
                camera.Priority = 0;
            else
            {
                foreach (CinemachineVirtualCamera c in cameras)
                {
                    if (c == activeCamera)
                        c.Priority = 1;
                    else if (c != activeCamera && c.Priority != 0)
                    {
                        c.Priority = 0;
                    }
                }
                camera.Priority = 10;
                
                activeCamera = camera;
                
            }
        }
    }

    public void RegisterCamera(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
    }
}

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public static class CameraController
{
    private static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    public static CinemachineVirtualCamera activeCamera = null;

    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == activeCamera;
    }
    
    public static void SwitchCamera(CinemachineVirtualCamera camera)
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

    public static void RegisterCamera(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
        Debug.Log("Added 1 camera to index");
    }
}

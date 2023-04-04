using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraRef;
    [SerializeField] private Transform zoomOutRef;
    [SerializeField] private float idleTimer = 5f;

    private Vector3 latestPos = Vector3.zero;
    
    void Update()
    {
        if (!Input.anyKey && idleTimer <= 0f)
        {
            MoveCamera(zoomOutRef.position);
            idleTimer = 5f;
        }
        else if (!Input.anyKey)
        {
            idleTimer -= 1 * Time.deltaTime;
        }
        else
        {
            MoveCamera(latestPos);
        }
    }

    public void MoveCamera(Vector3 target)
    {
        if (latestPos != Vector3.zero)
            if (target == latestPos)
                return;
        StopAllCoroutines();
        StartCoroutine(CameraMovement(target));
    }

    IEnumerator CameraMovement(Vector3 newPos)
    {
        Vector3 currentPos = cameraRef.transform.position;
        float time = 0f;
        float duration = 0.5f;
        latestPos = newPos;
        while (time < duration)
        {
            cameraRef.transform.position = Vector3.Slerp(currentPos, newPos, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraRef;
    [SerializeField] private Transform zoomOutRef;
    [SerializeField] private float idleTimer = 5f;
    [SerializeField] private AnimationCurve lerpCurve;

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
            idleTimer = 5f;
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
        float animationTimePosition = 0f;
        latestPos = newPos;
        while (cameraRef.position != newPos)
        {
            cameraRef.transform.position = Vector3.Lerp(currentPos, newPos, lerpCurve.Evaluate(animationTimePosition));
            animationTimePosition += 1 * Time.deltaTime;
            yield return null;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

public class Door : MonoBehaviour
{
    public Transform doorEnterSpot;
    public Transform doorExitSpot;
    public Transform openPosRef;
    public GameObject doorModel;
    public List<Guest> queue;
    private Vector3 closedPos;
    private Vector3 openPos;
    private float timer = 3f;
    public bool open;
    public bool closed = true;

    private void Start()
    {
        closedPos = doorModel.transform.position;
        openPos = openPosRef.position;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void OpenDoor()
    {
        StartCoroutine(OpenDoor(1f));
    }
    
    public void CloseDoor()
    {
        StartCoroutine(CloseDoor(1f));
    }
    
    private void Update()
    {
        if (open)
        {
            if (timer <= 0f)
            {
                CloseDoor();
                timer = 3f;
            }
            else
                timer -= 1f * Time.deltaTime;
        }
            
        // if (closed)
        // {
        //     if (Input.GetKeyDown(KeyCode.O))
        //         StartCoroutine(OpenDoor(1f));
        // }
        //
        // if (open)
        // {
        //     if (Input.GetKeyDown(KeyCode.C))
        //         StartCoroutine(CloseDoor(1f));
        // }
    }

    public IEnumerator OpenDoor(float moveTime)
    {
        closed = !closed;

        float time = 0;
        while (time < moveTime)
        {
            doorModel.transform.position = Vector3.Lerp(closedPos, openPos, time / moveTime);
            time += Time.deltaTime;
            yield return null;
        }
        open = !open;
    }

    public IEnumerator CloseDoor(float moveTime)
    {
        open = !open;
        float time = 0;
        while (time < moveTime)
        {
            doorModel.transform.position = Vector3.Lerp(openPos, closedPos, time / moveTime);
            time += Time.deltaTime;
            yield return null;
        }
        closed = !closed;
    }
}

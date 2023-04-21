using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Animator animator;
    private int objectsInDoorRange;


    private void Start()
    {
        closedPos = doorModel.transform.position;
        openPos = openPosRef.position;
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        // if (open)
        // {
        //     if (timer <= 0f)
        //     {
        //         CloseDoor();
        //         timer = 3f;
        //     }
        //     else
        //         timer -= 1f * Time.deltaTime;
        // }
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter");
        if (other.CompareTag("Guest") || other.CompareTag("Player") || other.CompareTag("boba eating guests"))
        {
            Debug.Log(other);
            objectsInDoorRange++;
            animator.SetTrigger("Open");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Guest") || other.CompareTag("Player") || other.CompareTag("boba eating guests"))
        {
            objectsInDoorRange--;
            if (objectsInDoorRange == 0)
                animator.SetTrigger("Close");
        }
    }

    public void ChangeOpenBool(int binary)
    {
        switch (binary)
        {
            case 0:
                if (open)
                    open = !open;
                break;
            case 1:
                if (!open)
                    open = !open;
                break;
        }
            
    }
}

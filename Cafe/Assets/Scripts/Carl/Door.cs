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
    public List<Guest> queue;
    public bool open;
    public Animator animator;
    private int objectsInDoorRange;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guest") || other.CompareTag("Player") || other.CompareTag("boba eating guests"))
        {
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

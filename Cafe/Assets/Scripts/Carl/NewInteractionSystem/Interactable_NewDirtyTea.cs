using System;
using System.Collections;
using System.Collections.Generic;
using Carl.NewInteractionSystem;
using UnityEngine;

public class Interactable_NewDirtyTea : NewAbstractInteractable
{
    private NewBobaTeaHandler tableRef;
    public bool dirtyDishOnTable;
    
    Vector3 startThrowPos;

    public Transform target;
    public Transform posOverHead;

    private float t = 0;

    bool isDishFlying;

    SoundManager soundManager;

    private void Start()
    {
        target = GameObject.Find("TargetPointDirtyDish").transform;
        posOverHead = GameObject.Find("PosOverHead").transform;
        soundManager = FindFirstObjectByType<SoundManager>();
    }
    private void Update()
    {
        Throw(playerInteractRef);
    }
    public override void Interact(NewInteract newInteract)
    {
        playerInteractRef = newInteract;
        if (dirtyDishOnTable)
        {
            dirtyDishOnTable = !dirtyDishOnTable;
            tableRef.hasDirtyDish = false;
        }
        
        if (isHeld)
        {
            startThrowPos = posOverHead.position;
            isDishFlying = true;
            t = 0;
            Throw(playerInteractRef);
            soundManager.Swoosh();
        }
        else
        {
            Hold(playerInteractRef);
        }
    }

    public override void Throw(NewInteract newInteract)
    {
        //Implement throw functionality
        if (isDishFlying)
        {

            // bobashooter script logic
            t += Time.deltaTime;
            toolParent.DetachChildren();

            //Implement throwing functionality
            float duration = 1.5f;
            float t01 = t / duration;

            // move to target
            Vector3 A = startThrowPos;
            Vector3 B = target.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            // move in arc
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);
            gameObject.transform.position = pos + arc;


            if (t01 >= 2)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                isDishFlying = false;
            }

            //Ensure the below function call is included
            playerInteractRef.NoLongerHoldingSomething();
        }
    }

    public void AddTableRef(NewBobaTeaHandler newBobaTeaHandler)
    {
        tableRef = newBobaTeaHandler;
        dirtyDishOnTable = true;
    }
    
    public override void TeaOperations(NewInteract newInteract)
    {
        //Not servable (gross)
    }

    public override void WaterOperations(NewInteract newInteract)
    {
        //Not a bucket
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pond"))
        {
            DestroyDish();
        }
    }

    private void DestroyDish()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Carl.NewInteractionSystem;
using UnityEngine;

public class Interactable_NewBoba : NewAbstractInteractable
{
    public Transform Target;
    Vector3 startThrowPos;
    private float t = 0;
    private void Start()
    {
        Target = GameObject.Find("TargetPoint Boba shooter").transform;
    }
    
    private void Update()
    {
        t += Time.deltaTime;
    }
    
    public override void Interact(NewInteract newInteract)
    {
        playerInteractRef = newInteract;

        if (isHeld)
        {
            Throw(playerInteractRef);
        }
        else
        {
            Hold(playerInteractRef);
        }
    }

    public override void Throw(NewInteract newInteract)
    {
        toolParent.DetachChildren();
        isHeld = false;
        //Implement throwing functionality
        float duration = 1.5f;
        float t01 = t / duration;

        // move to target
        Vector3 A = startThrowPos;
        Vector3 B = Target.position;
        Vector3 pos = Vector3.Lerp(A, B, t01);

        // move in arc
        Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);
        gameObject.transform.position = pos + arc;


        if (t01 >= 2)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        //Ensure the below function call is included
        playerInteractRef.NoLongerHoldingSomething();
    }

    public override void TeaOperations(NewInteract newInteract)
    {
        //Not servable
    }

    public override void WaterOperations(NewInteract newInteract)
    {
        //Not a bucket
    }
}

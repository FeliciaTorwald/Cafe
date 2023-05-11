using System.Collections;
using System.Collections.Generic;
using Carl.NewInteractionSystem;
using UnityEngine;

public class Interactable_NewBoba : NewAbstractInteractable
{
    Vector3 startThrowPos;

    public Transform target;
    public Transform posOverHead;

    private float t = 0;

    public bool isBallFlying;
    bool bobaIsStolen;

    SoundManager soundManager;
    Boba_guests_follow_boba boba_Guests_Follow_Boba;

    private void Start()
    {
        target = GameObject.Find("TargetPoint Boba shooter").transform;
        posOverHead = GameObject.Find("PosOverHead").transform;
        soundManager = FindFirstObjectByType<SoundManager>();
        boba_Guests_Follow_Boba = FindObjectOfType<Boba_guests_follow_boba>();
    }
    
    private void Update()
    {
        Throw(playerInteractRef);
    }
    
    public override void Interact(NewInteract newInteract)
    {
        playerInteractRef = newInteract;

        if (isHeld)
        {
            startThrowPos = posOverHead.position;
            isBallFlying = true;
            t = 0;
            Throw(playerInteractRef);
            isHeld = false;
            soundManager.BobaThrow();
            gameObject.GetComponent<BobaMovement>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<BobaMovement>().enabled = false;
            Hold(playerInteractRef);
        }

        if (bobaIsStolen)
        {
            playerInteractRef.NoLongerHoldingSomething();
        }
    }

    public override void Throw(NewInteract newInteract)
    {
        if (isBallFlying)
        {

            // bobashooter script logic
            gameObject.GetComponent<SphereCollider>().enabled = false;

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


            if (t01 >= 1)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                isBallFlying = false;

            }

            //Ensure the below function call is included
            playerInteractRef.NoLongerHoldingSomething();

        }
    }

    public override void TeaOperations(NewInteract newInteract)
    {
        //Not servable
    }

    public override void WaterOperations(NewInteract newInteract)
    {
        //Not a bucket
    }


    //check if bobatheif has taken your boba and tell player that they have lost it

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("BrewingPot"))
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            isBallFlying = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("BrewingPot"))
        {
            isBallFlying = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("BrewingPot"))
        {
            if(boba_Guests_Follow_Boba.ful_Hands == true)
            {
                bobaIsStolen = true;
            }

        }
    }


}

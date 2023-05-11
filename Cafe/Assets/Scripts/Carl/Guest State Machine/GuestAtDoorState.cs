using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.AI;

public class GuestAtDoorState : GuestState
{
    private bool moving;
    private bool askedForSeat;
    private float nextSeatCheckTimer = 3f;
    
    public GuestStateID GetID()
    {
        return GuestStateID.AtDoor;
    }

    public void Enter(Guest guest)
    {
        guest.door.queue.Add(guest);
    }

    public void Update(Guest guest)
    {
        if (!askedForSeat && !guest.chairRef)
        {
            LookForFreeSeat(guest);
            askedForSeat = !askedForSeat;
        }
        else if (guest.door.open && !guest.chairRef)
        {
            if (nextSeatCheckTimer <= 0f)
            {
                LookForFreeSeat(guest);
                nextSeatCheckTimer = 3f;
            }
            else
            {
                nextSeatCheckTimer -= 1 * Time.deltaTime;
            }
        }
        CheckIfAtDestination(guest);
    }

    public void Exit(Guest guest)
    {
        guest.door.queue.Remove(guest);
    }

    public void LookForFreeSeat(Guest guest)
    {
        if (GameManager.Instance.freeSeats > 0)
        {
            guest.chairRef = GameManager.Instance.AssignSeat();
            if (guest.chairRef)
                MoveToDestination(guest, guest.chairRef.transform);
        }
    }
    
    private void MoveToDestination(Guest guest, Transform target)
    {
        guest.navMeshAgent.destination = target.position;
        moving = true;
        
        //Moves the guest to a new position via navmesh system using a transform component reference.
    }
   
    private void CheckIfAtDestination(Guest guest)
    {
        if (moving && ReachedDestinationOrGaveUp(guest))
        {
            guest.navMeshAgent.ResetPath();
            moving = !moving;
            guest.stateMachine.ChangeState(GuestStateID.AtTable);
        }
    }
    
    public bool ReachedDestinationOrGaveUp(Guest guest)
    {
        if (!guest.navMeshAgent.pathPending)
        {
            if (guest.navMeshAgent.remainingDistance <= guest.navMeshAgent.stoppingDistance)
            {
                if (!guest.navMeshAgent.hasPath || guest.navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
        //This function will check if the guest has reached their destination.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.AI;

public class GuestAtDoorState : GuestState
{
    private bool moving;
    
    public GuestStateID GetID()
    {
        return GuestStateID.AtDoor;
    }

    public void Enter(Guest guest)
    {
        Debug.Log("Switched to At door state");
        guest.door.queue.Add(guest);
        //Adds this guest to the queue at the door. Functionality to be implemented.
    }

    public void Update(Guest guest)
    {
        if (guest.waitToBeSeatedTimer <= 0f)
        {
            LookForFreeSeat(guest);
        }
        else
        {
            guest.waitToBeSeatedTimer -= 1f * Time.deltaTime;
            Debug.Log(guest.waitToBeSeatedTimer);
        }

        CheckIfAtDestination(guest);
    }


    public void Exit(Guest guest)
    {
        Debug.Log("Left At door state");
    }

    private void LookForFreeSeat(Guest guest)
    {
        if (GameManager.Instance.freeSeats > 0)
        {
            MoveToDestination(guest, GameManager.Instance.AssignSeat().transform);
        }
    }
    
    private void MoveToDestination(Guest guest, Transform target)
    {
        Debug.Log("Moving to table");
        guest.navMeshAgent.destination = target.position;
        moving = true;
        
        //Moves the guest to a new position via navmesh system using a transform component reference.
    }
   
    private void CheckIfAtDestination(Guest guest)
    {
        if (moving && ReachedDestinationOrGaveUp(guest))
        {
            // if (guest.navMeshAgent.remainingDistance < 0.1f & guest.navMeshAgent.path.corners.Length == 0)
            // {
                guest.navMeshAgent.ResetPath();
                moving = !moving;
                guest.stateMachine.ChangeState(GuestStateID.AtTable);
            // }
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

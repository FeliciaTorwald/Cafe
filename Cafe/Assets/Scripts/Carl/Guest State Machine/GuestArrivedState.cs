using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuestArrivedState : GuestState
{
    private bool moving;
    
    public GuestStateID GetID()
    {
        return GuestStateID.Arrived;
    }

    public void Enter(Guest guest)
    {
        MoveToDestination(guest, guest.door.doorEnterSpot);
    }

    public void Update(Guest guest)
    {
        CheckIfAtDestination(guest);
    }
    
    public void Exit(Guest guest)
    {
        
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
            // if (guest.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && !guest.navMeshAgent.pathPending)
            // {
                guest.navMeshAgent.ResetPath();
                moving = !moving;
                guest.stateMachine.ChangeState(GuestStateID.AtDoor);
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

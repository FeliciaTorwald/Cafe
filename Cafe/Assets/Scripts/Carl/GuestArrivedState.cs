using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuestArrivedState : GuestState
{
    private NavMeshAgent navMeshAgent;
    
    public GuestStateID GetID()
    {
        return GuestStateID.Arrived;
    }

    public void Enter(Guest guest)
    {
        Debug.Log("Switched to Arrived state");
        MoveToDoor(guest, guest.door.transform);
    }

    public void Update(Guest guest)
    {
        CheckIfAtDoor(guest);
    }
    
    public void Exit(Guest guest)
    {
        Debug.Log("Left Arrived state");
    }
    
    private void MoveToDoor(Guest guest, Transform target)
    {
        guest.navMeshAgent.destination = target.position;
    }
    
    private void CheckIfAtDoor(Guest guest)
    {
        if (!guest.navMeshAgent.pathPending)
        {
            if (guest.navMeshAgent.remainingDistance <= guest.navMeshAgent.stoppingDistance)
            {
                if (!guest.navMeshAgent.hasPath || guest.navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    guest.stateMachine.ChangeState(GuestStateID.AtDoor);
                }
            }
        }
    }
}

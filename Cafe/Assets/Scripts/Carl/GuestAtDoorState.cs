using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class GuestAtDoorState : GuestState
{
    
    
    public GuestStateID GetID()
    {
        return GuestStateID.AtDoor;
    }

    public void Enter(Guest guest)
    {
        Debug.Log("Switched to At door state");
        guest.door.queue.Add(guest);
    }

    public void Update(Guest guest)
    {
        if (guest.waitToBeSeatedTimer <= 0f)
        {
            LookForFreeSeat(guest);
        }
        else
        {
            guest.waitToBeSeatedTimer = guest.waitToBeSeatedTimer - 1f * Time.deltaTime;
            Debug.Log(guest.waitToBeSeatedTimer);
        }
    }

    public void Exit(Guest guest)
    {
        Debug.Log("Left At door state");
    }

    private void LookForFreeSeat(Guest guest)
    {
        if (GameManager.Instance.freeSeats > 0)
        {
            MoveToTable(guest, GameManager.Instance.AssignSeat().transform);
        }
    }
    
    private void MoveToTable(Guest guest, Transform target)
    {
        Debug.Log("Moving to table");
        guest.navMeshAgent.destination = target.position;
    }
    
    private void CheckIfAtSeat(Guest guest)
    {
        if (!guest.navMeshAgent.pathPending)
        {
            if (guest.navMeshAgent.remainingDistance <= guest.navMeshAgent.stoppingDistance)
            {
                if (!guest.navMeshAgent.hasPath || guest.navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    guest.stateMachine.ChangeState(GuestStateID.AtTable);
                }
            }
        }
    }
    
}

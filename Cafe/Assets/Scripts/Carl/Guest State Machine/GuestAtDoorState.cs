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

        if (moving)
        {
            if (guest.navMeshAgent.remainingDistance < 0.1f)
            {
                guest.navMeshAgent.ResetPath();
                guest.stateMachine.ChangeState(GuestStateID.AtTable);
            }
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
        if (guest.navMeshAgent.remainingDistance > 1)
            moving = true;
    }
   
}

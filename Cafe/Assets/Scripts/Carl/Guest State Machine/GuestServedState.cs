using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestServedState : GuestState
{
    private bool moving;
    
    public GuestStateID GetID()
    {
        return GuestStateID.Served;
    }

    public void Enter(Guest guest)
    {
        guest.orderText.SetText("Served!");
        //guest.orderImg.RemoveOrderImage();
        MoveToDestination(guest, guest.door.doorExitSpot);
       // GameManager.Instance.ReturnFreeSeat(guest.chairRef);
        GameManager.Instance.AddServedGuest();
    }

    public void Update(Guest guest)
    {
        if (guest.door.open)
            guest.stateMachine.ChangeState(GuestStateID.Leaving);
        CheckIfAtDestination(guest);
        guest.guestCanvas.transform.forward = guest.camera.transform.forward;
    }

    public void Exit(Guest guest)
    {
        GameManager.Instance.freeSeats++;
        GameManager.Instance.ReturnFreeSeat(guest.chairRef);
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
            // guest.door.OpenDoor();
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

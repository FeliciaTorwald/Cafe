using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GuestLeavingState : GuestState
{
    private bool moving;
    
    public GuestStateID GetID()
    {
        return GuestStateID.Leaving;
    }

    public void Enter(Guest guest)
    {
        Debug.Log("Entered leaving state");
        MoveToDestination(guest, guest.guestSpawnPos);
        guest.orderText.SetText("Pax vobiscum, assholes");
        GameManager.Instance.RemoveGuest(guest);
    }

    public void Update(Guest guest)
    {
        guest.guestCanvas.transform.forward = guest.camera.transform.forward;
        CheckIfAtDestination(guest);
    }

    public void Exit(Guest guest)
    {
        
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
            guest.guestSpawner.SpawnNewGuest();
            guest.DespawnGuest();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guest : MonoBehaviour, IGuest
{
    [SerializeField] private List<Material> guestMaterial;
    public Door door;
    private NavMeshAgent navMeshAgent;
    public bool holding = false;
    
    public enum GuestType
    {
        TypeA,
        TypeB,
        TypeC
    }

    public enum GuestStatus
    {
        Arrived,
        AtDoor,
        AtTable,
        Ordered,
        Served,
        Leaving
    }

    public GuestStatus guestStatus;
    public GuestType guestType;

    private int numberOfStates;
    
    void Start()
    {
        numberOfStates = System.Enum.GetValues(typeof(GuestStatus)).Length;
        guestStatus = GuestStatus.Arrived;
    }
    
    void Update()
    {
        
        CheckIfMoving();
    }

    private void CheckIfMoving()
    {
        if (!holding)
        {
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        MoveToNextState();
                        holding = true;
                    }
                }
            }
        }
    }

    private void MoveToNextState()
    {
        guestStatus++;
    }

    public void OnSpawn()
    {
        MoveTo(door);
    }

    public void OnWaiting()
    {
        
    }
    
    public void OnOrder()
    {
        
        
    }

    public void OnOrderFulfilled()
    {
        
    }

    public void OnFinished()
    {
        
    }

    public void OnLeaving()
    {
        
    }

    private void MoveTo(Door target)
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = target.transform.position;
    }

    public Guest GetGameObject()
    {
        return this;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Guest : MonoBehaviour, IGuest
{
    [SerializeField] private List<Material> guestMaterial;
    [SerializeField] private Canvas guestCanvas;
    [SerializeField] private TMP_Text orderText;
    private Camera camera;
    public Door door;
    private NavMeshAgent navMeshAgent;
    public bool holding;
    public GameManager gameManager;
    private float waitToBeSeatedTimer = 2f;
    
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
        camera = Camera.main;
        guestCanvas.worldCamera = camera;
    }
    
    void Update()
    {
        CheckIfMoving();
        if (guestStatus == GuestStatus.AtDoor && holding)
        {
            if (waitToBeSeatedTimer <= 0f)
                LookForFreeSeat();
            else
            {
                waitToBeSeatedTimer -= 1f * Time.deltaTime;
            }
        }

        if (guestStatus == GuestStatus.AtTable && holding)
        {
            orderText.SetText("Tea 1");
        }

        guestCanvas.transform.forward = camera.transform.forward;

        // guestCanvas.transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
    }

    private void LookForFreeSeat()
    {
        if (gameManager.freeSeats > 0)
        {
            
            MoveTo(gameManager.AssignSeat().transform);
            holding = false;
        }
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
    
    public void OnSpawn()
    {
        MoveTo(door.transform);
    }

    public void OnAtDoor()
    {
        
    }
    
    public void OnAtTable()
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

    private void MoveTo(Transform target)
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = target.position;
    }
    
    private void MoveToNextState()
    {
        guestStatus++;
        if (guestStatus == GuestStatus.AtDoor)
            door.queue.Add(this);
    }
    
    public Guest GetGuestObject()
    {
        return this;
    }
    
    
}

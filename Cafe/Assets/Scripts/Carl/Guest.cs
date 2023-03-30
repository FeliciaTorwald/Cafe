using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Guest : MonoBehaviour
{
    public GuestStatemachine stateMachine;
    public GuestStateID initialState;
    public GuestConfig guestConfig;
    
    public Canvas guestCanvas;
    public TMP_Text orderText;
    
    public Camera camera;
    public Door door;
    public NavMeshAgent navMeshAgent;
    public float waitToBeSeatedTimer = 2f;
    
    public enum GuestType
    {
        TypeA,
        TypeB,
        TypeC
    }

    public GuestType guestType;

    void Start()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        camera = Camera.main;
        guestCanvas.worldCamera = camera;
        stateMachine = new GuestStatemachine(this);
        stateMachine.RegisterState(new GuestArrivedState());
        stateMachine.RegisterState(new GuestAtDoorState());
        stateMachine.RegisterState(new GuestAtTableState());
        stateMachine.SetInitialState(initialState);
    }
    
    void Update()
    {
        stateMachine.Update();
    }
    
}

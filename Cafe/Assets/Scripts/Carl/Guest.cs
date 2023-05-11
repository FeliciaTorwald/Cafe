using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Guest : MonoBehaviour
{
    public GuestStatemachine stateMachine;
    public GuestStateID initialState;
    public GuestConfig guestConfig;
    public GuestInteraction guestInteraction;

    public Transform guestSpawnPos;
    public TMP_FontAsset gibberishFont;
    public Canvas guestCanvas;
    public TMP_Text orderText;
    public Color angryTextColor;
    public Color happyTextColor;
    
    public new Camera camera;
    public Door door;
    public NavMeshAgent navMeshAgent;
    public float waitToBeSeatedTimer = 2f;
    public Chair chairRef;
    public OrderImageUI orderImg;
    public GameObject teaOrderImg;
    public GuestSpawner guestSpawner;
    public bool angry;
    public Animator animator;
    
    //Enum to identify type of guest, can be set in prefab or when instantiating guest
    public enum GuestType
    {
        TypeA,
        TypeB,
        TypeC
    }
    public GuestType guestType;
    
    //Determines what type of tea the guest wants, can be set when instantiating
    public TeaType teaType;

    void Start()
    {
        //Set reference camera for the canvas that will display what the guest has ordered
        
        //Create references for use in state machine state scripts
        navMeshAgent = GetComponent<NavMeshAgent>();
        orderImg = FindObjectOfType<OrderImageUI>();
        camera = Camera.main;
        
        guestCanvas.worldCamera = camera;
        
        //Register state machine and state machine states
        stateMachine = new GuestStatemachine(this);
        stateMachine.RegisterState(new GuestArrivedState());
        stateMachine.RegisterState(new GuestAtDoorState());
        stateMachine.RegisterState(new GuestAtTableState());
        stateMachine.RegisterState(new GuestOrderedState());
        stateMachine.RegisterState(new GuestServedState());
        stateMachine.RegisterState(new GuestLeavingState());
        stateMachine.RegisterState(new GuestAngryState());
        //Sets the initial state of the guest, should always be "Arriving"
        stateMachine.SetInitialState(initialState);
    }

    
    void Update()
    {
        stateMachine.Update();

    }
    
    public void DespawnGuest()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestStatemachine
{
    public GuestState[] states;
    public Guest guest;
    public GuestStateID currentState;

    public GuestStatemachine(Guest guest)
    {
        this.guest = guest;
        int numstates = System.Enum.GetNames(typeof(GuestStateID)).Length;
        states = new GuestState[numstates];
    }

    public void RegisterState(GuestState state)
    {
        int index = (int)state.GetID();
        states[index] = state;
        
        //Used to register states in the state machine so that they can be entered
    }

    public GuestState GetState(GuestStateID stateId)
    {
        int index = (int)stateId;
        return states[index];
        
        //Returns what state the guest is currently in, used to determine which state should be ran
    }
    
    public void Update()
    {
        GetState(currentState)?.Update(guest);
        
        //Runs the "Update" function in the current state's script
    }

    public void ChangeState(GuestStateID newState)
    {
        GetState(currentState)?.Exit(guest);
        currentState = newState;
        GetState(currentState)?.Enter(guest);
        
        //Changes what state the guest is in. First runs the "Exit" function in the current state, then changes
        //state and runs the "Enter" function in the new state.
    }

    public void SetInitialState(GuestStateID newState)
    {
        currentState = newState;
        GetState(currentState)?.Enter(guest);
        
        //Sets the initial state for the guest. Should always be "Arriving."
    }
}

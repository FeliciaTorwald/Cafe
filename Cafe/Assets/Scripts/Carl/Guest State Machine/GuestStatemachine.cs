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
    }

    public GuestState GetState(GuestStateID stateId)
    {
        int index = (int)stateId;
        return states[index];
    }
    
    public void Update()
    {
        GetState(currentState)?.Update(guest);
    }

    public void ChangeState(GuestStateID newState)
    {
        GetState(currentState)?.Exit(guest);
        currentState = newState;
        GetState(currentState)?.Enter(guest);
    }

    public void SetInitialState(GuestStateID newState)
    {
        currentState = newState;
        GetState(currentState)?.Enter(guest);
    }
}

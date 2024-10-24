using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MovementState
{
    Start,
    Wondering,
    CakeTargeted,
    Returning
}

public class AntState : MonoBehaviour
{
    public MovementState State { get; private set;}
    [SerializeField] string stateStr;

    private void Awake() {
        State = MovementState.Start;
        stateStr = State.ToString();
    }

    public void SetState(MovementState newState)
    {
        switch(newState)
        {
            case MovementState.Wondering:
                if(State != MovementState.Start && State != MovementState.CakeTargeted)
                    throw new InvalidOperationException($"Cannot change ant state from {State} to {newState}.");
                State = newState;
                break;
            case MovementState.CakeTargeted:
                if(State != MovementState.Start && State != MovementState.Wondering)
                    throw new InvalidOperationException($"Cannot change ant state from {State} to {newState}.");
                State = newState;
                break;
            case MovementState.Returning:
                if(State != MovementState.CakeTargeted)
                    throw new InvalidOperationException($"Cannot change ant state from {State} to {newState}.");
                State = newState;
                break;
        }
        stateStr = State.ToString();
    }

}

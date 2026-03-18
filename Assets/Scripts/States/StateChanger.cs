using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class StateChanger : Singleton<StateChanger>
{
    [SerializeField, Serialize] List<State> states;
    private State _currentState;

    private void OnEnable()
    {
        SetState("Move");
    }

    private void Update()
    {
        if (_currentState == null) return;

        _currentState.UpdateState();
    }

    public void SetState(string name)
    {
        foreach (var state in states)
        {
            if (state.Name == name)
            {
                _currentState?.OnExitState();
                _currentState = state;
                _currentState?.OnEnterState();
            }
        }
    }

}

using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class StateMachineHandler
    {
       
        private Stack<IState> _stateStack = new Stack<IState>();
        public IState CurrentState => _stateStack.Count > 0 ? _stateStack.Peek() : null;

        public void AddState(IState newState)
        {
            if (CurrentState != null) CurrentState.OnExit();

            _stateStack.Push(newState);
            CurrentState.OnEnter();
        }

        public void RemoveState()
        {
            if (CurrentState != null) CurrentState.OnExit();

            _stateStack.Pop();

            if (CurrentState != null) CurrentState.OnEnter();
        }

        public void UpdateStates()
        {
            if (CurrentState != null) CurrentState.OnUpdate();
        }
    }
}
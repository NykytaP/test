using System;
using System.Linq;
using Core.StateMachine.BaseStateMachine.States;

namespace Core.StateMachine.BaseStateMachine
{
    public class StateMachine
    {
        private readonly Action _onStateChangeCallback;
        
        private IState _currentState;
        private IState[] _states;
        private Transition[] _transitions;

        public StateMachine(IState[] states, Transition[] transitions, Action onStateChangeCallback)
        {
            _states = states;
            _transitions = transitions;
            _onStateChangeCallback = onStateChangeCallback;
            
            ChangeState(states[0].GetType());
        }

        public void FixedUpdate()
        {
            if (_currentState is IFixedUpdatableState updatableState)
            {
                updatableState.FixedUpdate();
            }

            foreach (Transition transition in _transitions)
            {
                if (transition.From == _currentState.GetType() && transition.Condition())
                {
                    ChangeState(transition.To);
                }
            }
        }

        private void ChangeState(Type to)
        {
            if(_currentState is IExitState exitState)
                exitState.Exit();

            _currentState = _states.First(x => x.GetType() == to);
            _onStateChangeCallback?.Invoke();
            
            if(_currentState is IEnterState enterState)
                enterState.Enter();
        }
    }
}
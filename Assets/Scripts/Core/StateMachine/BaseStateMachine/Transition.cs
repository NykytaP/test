using System;

namespace Core.StateMachine.BaseStateMachine
{
    public class Transition
    {
        public Transition(Type from, Type to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
        
        public Type From { get; }
        public Type To { get; }
        public Func<bool> Condition { get; }
    }
}
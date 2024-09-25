namespace Core.StateMachine.BaseStateMachine.States
{
    public interface IFixedUpdatableState : IState
    {
        public void FixedUpdate();
    }
}
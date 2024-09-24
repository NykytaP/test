using Cysharp.Threading.Tasks;

namespace Infrastructure.StateMachine.States
{
    public interface IState : IExitableState
    {
        public UniTask<AsyncUnit> Enter();
    }

    public interface IExitableState
    {
        public UniTask<AsyncUnit> Exit();
    }
    
    public interface IPayloadedState<TPayload> : IExitableState
    {
        public UniTask<AsyncUnit> Enter(TPayload payload);
    }
}
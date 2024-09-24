using Cysharp.Threading.Tasks;

namespace Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        public async UniTask<AsyncUnit> Enter()
        {
            return AsyncUnit.Default;
        }

        public async UniTask<AsyncUnit> Exit()
        {
            return AsyncUnit.Default;
        }
    }
}
using Cysharp.Threading.Tasks;

namespace Infrastructure.StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public LoadProgressState(GameStateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }

        public async UniTask<AsyncUnit> Enter()
        {
            await _stateMachine.Enter<LoadLevelState>();
            
            return AsyncUnit.Default;
        }

        public async UniTask<AsyncUnit> Exit()
        {
            return AsyncUnit.Default;
        }
    }
}
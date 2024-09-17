using System.Threading.Tasks;
namespace Infrastructure.StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public LoadProgressState(GameStateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }

        public async Task Enter()
        {
            await _stateMachine.Enter<LoadLevelState>();
        }

        public async Task Exit()
        {
        }
    }
}
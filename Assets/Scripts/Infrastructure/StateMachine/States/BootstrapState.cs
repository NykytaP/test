using System.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.SceneManagement;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
        }

        public async Task Enter()
        {
            ConfigureApp();
            await _uiFactory.InitPreloadingView();
            await _uiFactory.InitLoaderView();
            await _stateMachine.Enter<LoadProgressState>();
        }

        public async Task Exit()
        {
        }
        
        private void ConfigureApp()
        {
            Application.targetFrameRate = 120;
            Application.quitting += OnQuit;
        }
        
        private void OnQuit()
        {
        }
    }
}
using Cysharp.Threading.Tasks;
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

        public async UniTask<AsyncUnit> Enter()
        {
            ConfigureApp();
            await _uiFactory.InitPreloadingView();
            await _uiFactory.InitLoaderView();
            await _stateMachine.Enter<LoadProgressState>();
            
            return AsyncUnit.Default;
        }

        public async UniTask<AsyncUnit> Exit()
        {
            return AsyncUnit.Default;
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
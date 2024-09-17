using System.Threading.Tasks;
using Core.Views.Loaders;
using Data.Enums;
using Infrastructure.Factories;
using Infrastructure.Helpers.CancellationTokenHelper;
using Infrastructure.SceneManagement;
using Zenject;

namespace Infrastructure.StateMachine.States
{
    public class LoadLevelState : IState
    {
        private readonly DiContainer _container;
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly ICancellationTokenHelper _cancellationTokenHelper;

        private IPreloadingView _preloadingView;
        private ILoaderView _loaderView;

        public LoadLevelState(DiContainer container, GameStateMachine gameStateMachine, SceneLoader sceneLoader, 
            IUIFactory uiFactory, ICancellationTokenHelper cancellationTokenHelper)
        {
            _container = container;
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _cancellationTokenHelper = cancellationTokenHelper;
        }

        public async Task Enter()
        {
            PreloadingView.ShowLoading();
            LoaderView.HideLoading();
            
            await _sceneLoader.Load(SceneName.LevelScene, OnLoaded);
        }

        public async Task Exit()
        {
            PreloadingView.HideLoading();
        }
        
        private async void OnLoaded(SceneName sceneName)
        {
            await InitUIRoot();
            await InitGameWorld();
            await InitUI();
            await InitializeWorldEntities();
            
            await _stateMachine.Enter<GameLoopState>();
        }

        private async Task InitUIRoot()
        {
            await _uiFactory.InitUIRoot();
        }

        private async Task InitGameWorld()
        {
            await InitHero();
        }

        private async Task InitHero()
        {
        }

        private async Task InitUI()
        {
            await _uiFactory.CreateHUD();
        }

        private async Task InitializeWorldEntities()
        {
        }

        private IPreloadingView PreloadingView
        {
            get
            {
                if (_preloadingView == null)
                {
                    _preloadingView = _container.Resolve<IPreloadingView>();
                }

                return _preloadingView;
            }
        }
        
        private ILoaderView LoaderView
        {
            get
            {
                if (_loaderView == null)
                {
                    _loaderView = _container.Resolve<ILoaderView>();
                }

                return _loaderView;
            }
        }
    }
}
using Core.Pools.Bullets;
using Core.Pools.VFX;
using Core.Presenters.Player;
using Core.Views.Loaders;
using Cysharp.Threading.Tasks;
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
        private readonly IBulletsPoolManager _bulletsPoolManager;
        private readonly IVFXPoolManager _vfxPoolManager;

        private IPreloadingView _preloadingView;
        private ILoaderView _loaderView;

        public LoadLevelState(DiContainer container, GameStateMachine gameStateMachine, SceneLoader sceneLoader, 
            IUIFactory uiFactory, ICancellationTokenHelper cancellationTokenHelper, IBulletsPoolManager bulletsPoolManager, IVFXPoolManager vfxPoolManager)
        {
            _container = container;
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _cancellationTokenHelper = cancellationTokenHelper;
            _bulletsPoolManager = bulletsPoolManager;
            _vfxPoolManager = vfxPoolManager;
        }

        public async UniTask<AsyncUnit> Enter()
        {
            PreloadingView.ShowLoading();
            LoaderView.HideLoading();
            
            await _sceneLoader.Load(SceneName.LevelScene, OnLoaded);
            
            return AsyncUnit.Default;
        }

        public async UniTask<AsyncUnit> Exit()
        {
            PreloadingView.HideLoading();
            
            return AsyncUnit.Default;
        }
        
        private async void OnLoaded(SceneName sceneName)
        {
            await InitUIRoot();
            await InitGameWorld();
            await InitUI();
            await InitObjectPools();
            
            await _stateMachine.Enter<GameLoopState>();
        }

        private async UniTask<AsyncUnit> InitUIRoot()
        {
            await _uiFactory.InitUIRoot();
            
            return AsyncUnit.Default;
        }

        private async UniTask<AsyncUnit> InitGameWorld()
        {
            await InitHero();
            
            return AsyncUnit.Default;
        }

        private async UniTask<AsyncUnit> InitHero()
        {
            IPlayerPresenter playerPresenter = _container.Resolve<IPlayerPresenter>();

            await playerPresenter.InitializePlayer(_cancellationTokenHelper.GetSceneCancellationToken());
            
            return AsyncUnit.Default;
        }

        private async UniTask<AsyncUnit> InitUI()
        {
            await _uiFactory.CreateHUD();
            
            return AsyncUnit.Default;
        }
        
        private async UniTask<AsyncUnit> InitObjectPools()
        {
            await _bulletsPoolManager.InitPool(_cancellationTokenHelper.GetSceneCancellationToken());
            await _vfxPoolManager.InitPool(_cancellationTokenHelper.GetSceneCancellationToken());
            
            return AsyncUnit.Default;
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
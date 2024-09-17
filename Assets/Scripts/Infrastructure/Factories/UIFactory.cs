using System.Threading;
using System.Threading.Tasks;
using Core.Loaders.MainUI;
using Core.Views.Loaders;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.GameObjectHelper;
using Infrastructure.Helpers.UISceneHelper;
using UnityEngine;
using Zenject;
namespace Infrastructure.Factories
{
    public class UIFactory : IUIFactory, IUISceneHelper
    {
        private readonly DiContainer _container;
        private readonly IMainUILoader _mainUILoader;
        private readonly IGameObjectHelper _gameObjectHelper;

        private Canvas _uiRoot;

        public UIFactory(DiContainer container, IMainUILoader mainUILoader, IGameObjectHelper gameObjectHelper)
        {
            _container = container;
            _mainUILoader = mainUILoader;
            _gameObjectHelper = gameObjectHelper;
        }

        public async Task InitUIRoot()
        {
            Canvas prefab = await _mainUILoader.LoadUIRoot();
            _uiRoot = _gameObjectHelper.InstantiateObjectWithComponentInScene<Canvas>(prefab.gameObject);
        }
        
        public async Task InitPreloadingView()
        {
            PreloadingView prefab = await _mainUILoader.LoadPreloadingView();
            
            _container.Bind<IPreloadingView>().To<PreloadingView>().FromComponentInNewPrefab(prefab).AsSingle();
        }
        
        public async Task InitLoaderView()
        {
            LoaderView prefab = await _mainUILoader.LoadLoaderView();
            
            _container.Bind<ILoaderView>().To<LoaderView>().FromComponentInNewPrefab(prefab).AsSingle();
        }
        
        public async Task<GameObject> CreateHUD()
        {
            GameObject prefab = await _mainUILoader.LoadHud(GetSceneCancellationToken());
            
            return _gameObjectHelper.InstantiatePrefabInScene<GameObject>(prefab.gameObject, _uiRoot.transform, true); 
        }

        public void CleanUp()
        {
            _mainUILoader.Dispose();
        }

        public async UniTask<Transform> GetUIRoot()
        {
            if (!_uiRoot)
            {
                await InitUIRoot();
            }

            return _uiRoot.transform;
        }
        
        public CancellationToken GetSceneCancellationToken()
        {
            return RootGameObjectInstance.GetCancellationTokenOnDestroy();
        }

        public GameObject RootGameObjectInstance => _uiRoot.gameObject;
    }
}
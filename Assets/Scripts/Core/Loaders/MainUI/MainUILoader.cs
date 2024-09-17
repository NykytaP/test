using System.Threading;
using Core.Views.Loaders;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetsManagement;
using Infrastructure.Helpers.Loaders;
using UnityEngine;

namespace Core.Loaders.MainUI
{
    public class MainUILoader : LoaderBase, IMainUILoader
    {
        private const string UIRootPath = "UI/UIRootPrefab";
        private const string HudPrefabId = "UI/HudPrefab";
        private const string PreloadingViewPath = "UI/PreloadingView";
        private const string LoaderViewPath = "UI/LoaderView";

        public MainUILoader(IAssetProvider assetsProvider)
            : base(assetsProvider)
        {
        }
        
        public UniTask<PreloadingView> LoadPreloadingView()
        {
            return LoadComponentFromAssetGameObject<PreloadingView>(PreloadingViewPath, new CancellationToken());
        }
        
        public UniTask<LoaderView> LoadLoaderView()
        {
            return LoadComponentFromAssetGameObject<LoaderView>(LoaderViewPath, new CancellationToken());
        }
        
        public UniTask<Canvas> LoadUIRoot()
        {
            return LoadComponentFromAssetGameObject<Canvas>(UIRootPath, new CancellationToken());
        }

        public UniTask<GameObject> LoadHud(CancellationToken cancellationToken)
        {
            return LoadAsset<GameObject>(HudPrefabId, cancellationToken);
        }
    }
}
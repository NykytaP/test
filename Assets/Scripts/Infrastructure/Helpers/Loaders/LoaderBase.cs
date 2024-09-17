using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetsManagement;
using UnityEngine;
using Object = UnityEngine.Object;
namespace Infrastructure.Helpers.Loaders
{
    public class LoaderBase : ILoader
    {
        private readonly IAssetProvider _assetProvider;
        
        protected List<object> cachedAssets;

        public LoaderBase(IAssetProvider assetsProvider)
        {
            _assetProvider = assetsProvider;

            cachedAssets = new List<object>();
        }

        protected async UniTask<T> LoadAsset<T>(string assetKey, CancellationToken cancellationToken) 
            where T : Object
        {
            var loadedAsset = await _assetProvider.LoadAsync<T>(assetKey);
            
            if (cancellationToken.IsCancellationRequested)
            {
                _assetProvider.ReleaseAsset(loadedAsset);
                return null;
            }

            cachedAssets.Add(loadedAsset);
            return loadedAsset;
        }

        protected async UniTask<T> LoadComponentFromAssetGameObject<T>(string assetKey,
            CancellationToken cancellationToken)  
            where T : Object
        {
            var loadedAsset = await _assetProvider.LoadAsync<GameObject>(assetKey);
            if (cancellationToken.IsCancellationRequested)
            {
                _assetProvider.ReleaseAsset(loadedAsset);
                return null;
            }

            cachedAssets.Add(loadedAsset);
            return loadedAsset.GetComponent<T>();
        }

        public void Dispose()
        {
            foreach (object cachedAsset in cachedAssets)
            {
                try
                {
                    _assetProvider.ReleaseAsset(cachedAsset);
                }
                catch (Exception e)
                {
                    Debug.LogError(cachedAsset);
                    throw;
                }
            }

            cachedAssets.Clear();

            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
    }
}
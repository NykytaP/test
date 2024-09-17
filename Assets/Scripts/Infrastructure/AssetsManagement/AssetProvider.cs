using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
namespace Infrastructure.AssetsManagement
{
    public class AssetProvider : IAssetProvider
    {
        public UniTask<T> LoadAsync<T>(string assetKey)
        {
            return Addressables.LoadAssetAsync<T>(assetKey).ToUniTask();
        }

        public UniTask LoadSceneAsync(string sceneKey)
        {
            return Addressables.LoadSceneAsync(sceneKey).ToUniTask();
        }

        public void ReleaseAsset<T>(T loadedResource)
        {
            Addressables.Release(loadedResource);
        }

        public UniTask<long> GetSize(string assetKey)
        {
            return Addressables.GetDownloadSizeAsync(assetKey).ToUniTask();
        }

        public bool AddressableResourceExists(object key)
        {
            return Addressables.ResourceLocators.Any(l => l.Locate(key, typeof(object), out _));
        }

        public async UniTask<bool> IsAssetLoaded(string assetKey)
        {
            var assetSize = await GetSize(assetKey);
            return assetSize == 0;
        }

        public async UniTask<AsyncUnit> DownloadAsset(string assetKey, IProgress<float> progress = null)
        {
            await Addressables.DownloadDependenciesAsync(assetKey, true).ToUniTask(progress);
            return AsyncUnit.Default;
        }
    }
}
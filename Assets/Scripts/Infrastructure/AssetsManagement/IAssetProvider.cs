using System;
using Cysharp.Threading.Tasks;
namespace Infrastructure.AssetsManagement
{
    public interface IAssetProvider
    {
        UniTask<T> LoadAsync<T>(string assetKey);

        UniTask LoadSceneAsync(string sceneKey);

        void ReleaseAsset<T>(T loadedResource);

        UniTask<long> GetSize(string assetKey);
        
        bool AddressableResourceExists(object key);

        UniTask<bool> IsAssetLoaded(string assetKey);

        UniTask<AsyncUnit> DownloadAsset(string assetKey, IProgress<float> progress = null);
    }
}
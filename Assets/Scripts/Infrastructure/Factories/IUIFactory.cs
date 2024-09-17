using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
namespace Infrastructure.Factories
{
    public interface IUIFactory
    {
        public Task InitUIRoot();
        public Task InitPreloadingView();
        public Task InitLoaderView();
        public Task<GameObject> CreateHUD();
        public void CleanUp();
        public UniTask<Transform> GetUIRoot();
        public CancellationToken GetSceneCancellationToken();
    }
}
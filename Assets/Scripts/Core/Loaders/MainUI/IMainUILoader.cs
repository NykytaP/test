using System.Threading;
using Core.Views.Loaders;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.Loaders;
using UnityEngine;

namespace Core.Loaders.MainUI
{
    public interface IMainUILoader : ILoader
    {
        public UniTask<PreloadingView> LoadPreloadingView();
        public UniTask<LoaderView> LoadLoaderView();
        public UniTask<Canvas> LoadUIRoot();
        public UniTask<GameObject> LoadHud(CancellationToken cancellationToken);
    }
}
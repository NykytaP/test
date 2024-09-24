using System.Threading;
using Core.Views.Tank;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetsManagement;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.BotLoader
{
    public class BotLoader : LoaderBase, IBotLoader
    {
        private const string BotPath = "GameEntities/Bot";

        public BotLoader(IAssetProvider assetsProvider)
            : base(assetsProvider)
        {
        }
        
        public UniTask<TankViewContainer> LoadBotPrefab()
        {
            return LoadComponentFromAssetGameObject<TankViewContainer>(BotPath, new CancellationToken());
        }
    }
}
using System.Threading;
using Core.Views.Tank;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetsManagement;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.Player
{
    public class PlayerLoader : LoaderBase, IPlayerLoader
        {
        private const string PlayerPath = "GameEntities/Player";

        public PlayerLoader(IAssetProvider assetsProvider)
            : base(assetsProvider)
        {
        }
        
        public UniTask<TankViewContainer> LoadPlayerPrefab()
        {
            return LoadComponentFromAssetGameObject<TankViewContainer>(PlayerPath, new CancellationToken());
        }
    }
}
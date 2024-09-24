using System.Threading;
using Core.Views.Bullets;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetsManagement;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.Bullets
{
    public class BulletsLoader : LoaderBase, IBulletsLoader
    {
        private const string DefaultBulletPath = "GameEntities/Bullets/DefaultBullet";

        public BulletsLoader(IAssetProvider assetsProvider)
            : base(assetsProvider)
        {
        }
        
        public UniTask<BulletView> LoadDefaultBullet()
        {
            return LoadComponentFromAssetGameObject<BulletView>(DefaultBulletPath, new CancellationToken());
        }
    }
}
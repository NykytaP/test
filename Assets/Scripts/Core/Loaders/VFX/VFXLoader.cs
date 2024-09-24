using System.Threading;
using Core.Views.VFX;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetsManagement;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.VFX
{
    public class VFXLoader : LoaderBase, IVFXLoader
    {
        private const string HitVFXPath = "VFX/HitVFX";

        public VFXLoader(IAssetProvider assetsProvider)
            : base(assetsProvider)
        {
        }
        
        public UniTask<ParticleView> LoadHitVFX()
        {
            return LoadComponentFromAssetGameObject<ParticleView>(HitVFXPath, new CancellationToken());
        }
    }
}
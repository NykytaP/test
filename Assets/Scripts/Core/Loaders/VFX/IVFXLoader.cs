using Core.Views.VFX;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.VFX
{
    public interface IVFXLoader : ILoader
    {
        public UniTask<ParticleView> LoadHitVFX();
    }
}
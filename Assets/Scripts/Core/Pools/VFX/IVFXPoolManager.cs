using System.Threading;
using Core.Views.VFX;
using Cysharp.Threading.Tasks;

namespace Core.Pools.VFX
{
    public interface IVFXPoolManager
    {
        public UniTask<AsyncUnit> InitPool(CancellationToken cancellationToken);
        public ParticleView GetHitVFX();
    }
}
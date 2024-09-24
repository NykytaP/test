using System.Threading;
using Core.Views.Bullets;
using Cysharp.Threading.Tasks;

namespace Core.Pools.Bullets
{
    public interface IBulletsPoolManager
    {
        public UniTask<AsyncUnit> InitPool(CancellationToken cancellationToken);
        public BulletView GetBullet();
    }
}
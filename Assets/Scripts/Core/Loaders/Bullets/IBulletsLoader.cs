using Core.Views.Bullets;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.Bullets
{
    public interface IBulletsLoader : ILoader
    {
        public UniTask<BulletView> LoadDefaultBullet();
    }
}
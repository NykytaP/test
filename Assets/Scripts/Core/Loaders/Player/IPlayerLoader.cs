using Core.Views.Tank;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.Player
{
    public interface IPlayerLoader : ILoader
    {
        public UniTask<TankViewContainer> LoadPlayerPrefab();
    }
}
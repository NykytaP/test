using Core.Views.Tank;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.Loaders;

namespace Core.Loaders.BotLoader
{
    public interface IBotLoader : ILoader
    {
        public UniTask<TankViewContainer> LoadBotPrefab();
    }
}
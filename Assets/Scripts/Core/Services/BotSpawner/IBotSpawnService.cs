using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.Services.BotSpawner
{
    public interface IBotSpawnService
    {
        public UniTask<AsyncUnit> InitializeSpawner(CancellationToken cancellationToken);
    }
}
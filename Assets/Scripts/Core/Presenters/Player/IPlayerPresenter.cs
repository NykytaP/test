using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.Presenters.Player
{
    public interface IPlayerPresenter
    {
        public UniTask<AsyncUnit> InitializePlayer(CancellationToken cancellationToken);
    }
}
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Presenters.Bot
{
    public interface IBotPresenter
    {
        public UniTask<AsyncUnit> InitializeBot(Transform botsRoot, Action deathCallback, CancellationToken cancellationToken);
        public UniTask<AsyncUnit> SpawnBot();
        public bool IsAlive();
    }
}
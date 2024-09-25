using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Presenters.Bot;
using Core.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.Services.BotSpawner
{
    public class BotSpawnService : IBotSpawnService
    {
        private const int BotsAmount = 4;

        private readonly IBotPresenter[] _botPresenters;

        private CancellationToken _cancellationToken;
        private Transform _botsRoot;
        
        public BotSpawnService(DiContainer container)
        {
            _botPresenters = new IBotPresenter[BotsAmount];

            for (int i = 0; i < BotsAmount; i++)
                _botPresenters[i] = container.Resolve<IBotPresenter>();
        }
        
        public async UniTask<AsyncUnit> InitializeSpawner(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            
            foreach (IBotPresenter botPresenter in _botPresenters)
            {
                await botPresenter.InitializeBot(BotsRoot, OnDeathHandler, _cancellationToken);
            }
            
            return AsyncUnit.Default;
        }

        private async void OnDeathHandler()
        {
            if (_botPresenters.All(x => !x.IsAlive()))
            {
                try { await Task.Delay((int)(Constants.GameConstants.BotDeathAwait * 1000), _cancellationToken); }
                catch (Exception e) { }
                
                if(_cancellationToken.IsCancellationRequested)
                    return;
                
                RespawnBots();
            } 
        }

        private void RespawnBots()
        {
            foreach (IBotPresenter botPresenter in _botPresenters)
                botPresenter.SpawnBot();
        }

        private Transform BotsRoot
        {
            get
            {
                if (!_botsRoot)
                {
                    const string botRootName = "BotsRoot";
                    _botsRoot = new GameObject().transform;
                    _botsRoot.gameObject.name = botRootName;
                }

                return _botsRoot;
            }
        }
    }
}
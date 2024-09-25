using System;
using System.Threading;
using Core.Factories;
using Core.Services.Movement.Bot;
using Core.Utils;
using Core.Views.Tank;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Presenters.Bot
{
    public class BotPresenter : IBotPresenter
    {
        private readonly ITanksFactory _tanksFactory;

        private Action _onDeathCallback;
        private CancellationToken _cancellationToken;
        private ITankViewContainer _tankViewContainer;
        private IBotMovement _botMovement;
        private Transform _botsRoot;
        private bool _initialized;

        public BotPresenter(ITanksFactory tanksFactory, IBotMovement botMovement)
        {
            _tanksFactory = tanksFactory;
            _botMovement = botMovement;
        }
        
        public async UniTask<AsyncUnit> InitializeBot(Transform botsRoot, Action deathCallback, CancellationToken cancellationToken)
        {
            _initialized = true;
            _onDeathCallback = deathCallback;
            _botsRoot = botsRoot;
            _cancellationToken = cancellationToken;
            
            await SpawnBot();
            
            if(cancellationToken.IsCancellationRequested)
                return AsyncUnit.Default;
            
            _botMovement.Initialize(_tankViewContainer);
            _tankViewContainer.Damagable.OnDeathCallback += OnDeathHandler;

            return AsyncUnit.Default;
        }

        public async UniTask<AsyncUnit> SpawnBot()
        {
            if (!_initialized)
                return AsyncUnit.Default;
            
            if (_tankViewContainer == default)
            {
                _tankViewContainer = await _tanksFactory.SpawnBot(_botsRoot);
                
                if(_cancellationToken.IsCancellationRequested)
                    return AsyncUnit.Default;
            }

            _tankViewContainer.Entity.position = LevelUtils.GetRandomPointInBoxCollider(_tanksFactory.SpawnZone, _tankViewContainer.BoxCollider);
            _tankViewContainer.Entity.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            
            ResetBot();
            
            return AsyncUnit.Default;
        }
        
        public bool IsAlive()
        {
            return _tankViewContainer.Damagable.IsAlive();
        }
        
        private void OnDeathHandler()
        {
            _tankViewContainer.TankDeathView?.BlowUpTurret();
            _onDeathCallback?.Invoke();
        }
        
        private void ResetBot()
        {
            _tankViewContainer.Damagable.ResetHealth();
            _tankViewContainer.TankDeathView?.ResetTurret();
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Core.Factories;
using Core.Services.Movement.Player;
using Core.Services.Shooting.Player;
using Core.Utils;
using Core.Views.Tank;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Presenters.Player
{
    public class PlayerPresenter : IPlayerPresenter
    {
        private readonly ITanksFactory _tanksFactory;
        private readonly IPlayerShootingService _playerShootingService;

        private CancellationToken _cancellationToken;
        private ITankViewContainer _tankViewContainer;
        private IPlayerMovement _playerMovement;
        private Transform _playerRoot;

        public PlayerPresenter(ITanksFactory tanksFactory, IPlayerMovement playerMovement, IPlayerShootingService playerShootingService)
        {
            _tanksFactory = tanksFactory;
            _playerMovement = playerMovement;
            _playerShootingService = playerShootingService;
        }
        
        public async UniTask<AsyncUnit> InitializePlayer(CancellationToken cancellationToken)
        {
            await SpawnPlayer(cancellationToken);
            
            if(cancellationToken.IsCancellationRequested)
                return AsyncUnit.Default;

            _cancellationToken = cancellationToken;
            
            _playerMovement.Initialize(_tankViewContainer);
            _playerShootingService.Initialize(_tankViewContainer);
            _tankViewContainer.Damagable.OnDeathCallback += OnDeathHandler;

            return AsyncUnit.Default;
        }

        private async UniTask<AsyncUnit> SpawnPlayer(CancellationToken cancellationToken)
        {
            if (_tankViewContainer == default)
            {
                _tankViewContainer = await _tanksFactory.SpawnPlayer(PlayerRoot);
                
                if(cancellationToken.IsCancellationRequested)
                    return AsyncUnit.Default;
            }

            _tankViewContainer.Entity.position = LevelUtils.GetRandomPointInBoxCollider(_tanksFactory.SpawnZone, _tankViewContainer.BoxCollider);
            _tankViewContainer.Entity.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            
            ResetPlayer();
            
            return AsyncUnit.Default;
        }
        
        private async void OnDeathHandler()
        {
            _playerMovement.BlockInput = true;
            _tankViewContainer.TankDeathView?.BlowUpTurret();
            
            await Task.Delay((int)(Constants.GameConstants.DeathAwait * 1000), _cancellationToken);

            await SpawnPlayer(_cancellationToken);
        }
        
        private void ResetPlayer()
        {
            _tankViewContainer.Damagable.ResetHealth();
            _playerMovement.BlockInput = false;
            _tankViewContainer.TankDeathView?.ResetTurret();
        }

        private Transform PlayerRoot
        {
            get
            {
                if (!_playerRoot)
                {
                    const string playerRootName = "PlayerRoot";
                    _playerRoot = new GameObject().transform;
                    _playerRoot.gameObject.name = playerRootName;
                }

                return _playerRoot;
            }
        }
    }
}
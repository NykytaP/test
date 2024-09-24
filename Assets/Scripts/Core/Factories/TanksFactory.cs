using System;
using Core.Loaders.BotLoader;
using Core.Loaders.Player;
using Core.Utils;
using Core.Views.Tank;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.GameObjectHelper;
using UnityEngine;

namespace Core.Factories
{
    public class TanksFactory: ITanksFactory, IDisposable
    {
        private readonly IGameObjectHelper _gameObjectHelper;
        private readonly IPlayerLoader _playerLoader;
        private readonly IBotLoader _botLoader;

        private BoxCollider _spawnZone;

        public TanksFactory(IGameObjectHelper gameObjectHelper, IPlayerLoader playerLoader, IBotLoader botLoader)
        {
            _gameObjectHelper = gameObjectHelper;
            _playerLoader = playerLoader;
            _botLoader = botLoader;
        }
        
        public async UniTask<ITankViewContainer> SpawnPlayer(Transform root)
        {
            TankViewContainer prefab = await _playerLoader.LoadPlayerPrefab();

            return SpawnTankEntity(prefab, root);
        }
        
        public async UniTask<ITankViewContainer> SpawnBot(Transform root)
        {
            TankViewContainer prefab = await _botLoader.LoadBotPrefab();

            return SpawnTankEntity(prefab, root);
        }

        private ITankViewContainer SpawnTankEntity(TankViewContainer entityPrefab, Transform root)
        {
            return _gameObjectHelper.InstantiateObjectWithComponentInScene<TankViewContainer>(entityPrefab.gameObject, root);
        }
        
        public void Dispose()
        {
            _playerLoader.Dispose();
            _botLoader.Dispose();
        }

        public BoxCollider SpawnZone
        {
            get
            {
                if (!_spawnZone)
                {
                    _spawnZone = GameObject.FindWithTag(Constants.GameConstants.SpawnZoneTag).GetComponent<BoxCollider>();
                }

                return _spawnZone;
            }
        }
    }
}
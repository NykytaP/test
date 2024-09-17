﻿using System;
using Cysharp.Threading.Tasks;
using Data.Enums;
using Infrastructure.AssetsManagement;

namespace Infrastructure.SceneManagement
{
    public class SceneLoader
    {
        private readonly IAssetProvider _assetProvider;
            
        public SceneLoader(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask Load(SceneName sceneName, Action<SceneName> onLoaded = null)
        {
            await _assetProvider.LoadSceneAsync(sceneName.ToString());
            onLoaded?.Invoke(sceneName);
        }
    }
}
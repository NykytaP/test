using System.Threading;
using Core.Loaders.VFX;
using Core.Utils;
using Core.Views.VFX;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.ViewManager;
using Infrastructure.ObjectPool;
using UnityEngine;

namespace Core.Pools.VFX
{
    public class VFXPoolManager : ViewPresenterBase, IVFXPoolManager
    {
        private readonly IVFXLoader _vfxLoader;
        private Transform _poolsRoot;

        #region Pools
        
        private readonly IObjectPool<ParticleView> _pool;
        
        #endregion

        public VFXPoolManager(IVFXLoader vfxLoader, IObjectPool<ParticleView> itemPool)
        {
            _vfxLoader = vfxLoader;
            _pool = itemPool;
        }

        public async UniTask<AsyncUnit> InitPool(CancellationToken cancellationToken)
        {
            InitPoolsRoot();
            AddToDisposables(() => _vfxLoader.Dispose());

            CancellationTokenSource viewTokenSource = new CancellationTokenSource();
            RegisterToken(cancellationToken, viewTokenSource.Token);

            Transform root = CreateRoot(nameof(ParticleView), _poolsRoot);
            ParticleView itemPrefab = await _vfxLoader.LoadHitVFX();

            if (_cachedCancellationToken.IsCancellationRequested)
                return AsyncUnit.Default;

            _pool.InitPool(itemPrefab, root);

            return AsyncUnit.Default;
        }
        
        public ParticleView GetHitVFX()
        {
            ParticleView vfx = _pool.GetObject();
            vfx.Released = false;

            return vfx;
        }
        
        private void InitPoolsRoot()
        {
            if(_poolsRoot)
                return;

            _poolsRoot = GameObject.Find(Constants.Infrastructure.PoolRootName)?.transform;
            
            if(!_poolsRoot)
                _poolsRoot = CreateRoot(Constants.Infrastructure.PoolRootName);
        }

        private Transform CreateRoot(string name, Transform parent = null)
        {
            Transform root = new GameObject().transform;
            root.gameObject.name = name;
            
            if(parent)
                root.SetParent(parent);
            
            return root;
        }
    }
}
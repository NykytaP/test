using System.Threading;
using Core.Loaders.Bullets;
using Core.Utils;
using Core.Views.Bullets;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.ViewManager;
using Infrastructure.ObjectPool;
using UnityEngine;

namespace Core.Pools.Bullets
{
    public class BulletsPoolManager : ViewPresenterBase, IBulletsPoolManager
    {
        private readonly IBulletsLoader _bulletsLoader;
        private Transform _poolsRoot;

        #region Pools
        
        private readonly IObjectPool<BulletView> _pool;
        
        #endregion

        public BulletsPoolManager(IBulletsLoader bulletsLoader, IObjectPool<BulletView> itemPool)
        {
            _bulletsLoader = bulletsLoader;
            _pool = itemPool;
        }

        public async UniTask<AsyncUnit> InitPool(CancellationToken cancellationToken)
        {
            InitPoolsRoot();
            AddToDisposables(() => _bulletsLoader.Dispose());

            CancellationTokenSource viewTokenSource = new CancellationTokenSource();
            RegisterToken(cancellationToken, viewTokenSource.Token);

            Transform root = CreateRoot(nameof(BulletView), _poolsRoot);
            BulletView itemPrefab = await _bulletsLoader.LoadDefaultBullet();

            if (_cachedCancellationToken.IsCancellationRequested)
                return AsyncUnit.Default;

            _pool.InitPool(itemPrefab, root);

            return AsyncUnit.Default;
        }
        
        public BulletView GetBullet()
        {
            BulletView bullet = _pool.GetObject();
            bullet.Released = false;

            return bullet;
        }
        
        private void InitPoolsRoot()
        {
            if(_poolsRoot)
                return;

            _poolsRoot = GameObject.Find(Constants.Infrastructure.PoolRootName)?.transform;
            
            if(!_poolsRoot)
                _poolsRoot = CreateRoot(Constants.Infrastructure.PoolRootName);          //todo extract
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
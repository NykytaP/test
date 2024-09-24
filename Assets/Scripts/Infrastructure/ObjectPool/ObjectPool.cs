using System.Collections.Generic;
using Infrastructure.Helpers.GameObjectHelper;
using UnityEngine;

namespace Infrastructure.ObjectPool
{
    public class ObjectPool<T> : IObjectPool<T> 
        where T: MonoBehaviour, IPoolable
    {
        private const int PoolPrewarmCount = 20;
        
        private readonly IGameObjectHelper _gameObjectHelper;
        private readonly List<T> _objectsPool = new();

        private T _prefab;
        private Transform _parent;

        public ObjectPool(IGameObjectHelper gameObjectHelper)
        {
            _gameObjectHelper = gameObjectHelper;
        }

        public void InitPool(T prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;

            PrewarmPool();
        }

        public T GetObject()
        {
            foreach (T obj in _objectsPool)
            {
                if (obj.Released)
                {
                    return obj;
                }
            }
            
            return InstantiateObject();
        }

        private void PrewarmPool()
        {
            for (int i = 0; i < PoolPrewarmCount; i++)
            {
                InstantiateObject();
            }
        }

        public void ReleaseObject(T obj)
        {
            obj.Release();
        }

        private T InstantiateObject() 
        {
            T obj = _gameObjectHelper.InstantiateObjectWithComponentInScene<T>(_prefab.GameObject, _parent, false);
            _objectsPool.Add(obj);
            
            obj.Release();

            return obj;
        }
    }
}
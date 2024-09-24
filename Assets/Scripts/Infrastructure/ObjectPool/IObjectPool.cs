using UnityEngine;

namespace Infrastructure.ObjectPool
{
    public interface IObjectPool<T>
        where T: IPoolable
    {
        public void InitPool(T prefab, Transform parent);
        public T GetObject();
        public void ReleaseObject(T obj);
    }
}
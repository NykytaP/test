using UnityEngine;

namespace Infrastructure.ObjectPool
{
    public interface IPoolable
    {
        public void Release();
        public bool Released { get; }
        public GameObject GameObject { get; }
    }
}
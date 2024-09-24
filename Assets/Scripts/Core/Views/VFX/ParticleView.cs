using System;
using System.Collections;
using Infrastructure.ObjectPool;
using UnityEngine;

namespace Core.Views.VFX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleView : MonoBehaviour, IPoolable
    {
        private ParticleSystem _particleSystem;
        private bool _released;

        private void Awake()
        {
            InitComponents();
        }

        public void PlayVFX()
        {
            _particleSystem.Play();

            StartCoroutine(DelayRelease());
        }

        private IEnumerator DelayRelease()
        {
            yield return new WaitUntil(() => !_particleSystem.isEmitting);
            
            Release();
        }

        public void Release()
        {
            _released = true;
            _particleSystem.Stop();
            gameObject.SetActive(false);
        }
        
        private void InitComponents()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public bool Released
        {
            get
            {
                return _released && !gameObject.activeInHierarchy;
            }
            set
            {
                _released = value;
            }
        }
        public GameObject GameObject => gameObject;
    }
}
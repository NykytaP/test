using System;
using System.Collections;
using Core.Utils;
using Infrastructure.ObjectPool;
using UnityEngine;

namespace Core.Views.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletView : MonoBehaviour, IPoolable
    {
        public event Action<Collision, Quaternion> OnCollisionEnterAction;

        private Coroutine _timeDeathCoroutine;
        private TrailRenderer _trailRenderer;
        private Rigidbody _rb;
        private bool _released;

        private void Awake()
        {
            InitComponents();
        }

        private void OnEnable()
        {
            Reset();
            _timeDeathCoroutine = StartCoroutine(TimeDeath());
        }

        private void FixedUpdate()
        {
            ApplySpeed();
        }

        private void OnCollisionEnter(Collision other)
        {
            Release();
            
            Quaternion vfxRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180f, 0);
            
            OnCollisionEnterAction?.Invoke(other, vfxRotation);
            OnCollisionEnterAction = null;
            
            if(_timeDeathCoroutine != null)
                StopCoroutine(_timeDeathCoroutine);
        }
        
        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void Release()
        {
            gameObject.SetActive(false);
            _released = true;
        }
        
        private void InitComponents()
        {
            _rb = GetComponent<Rigidbody>();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
        }
        
        private void Reset()
        {
            _trailRenderer.Clear();
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
        
        private void ApplySpeed()
        {
            _rb.velocity = transform.forward * Constants.GameConstants.BulletSpeed;
        }

        private IEnumerator TimeDeath()
        {
            yield return new WaitForSeconds(Constants.GameConstants.BulletLifetime);
            
            Release();
        }
        
        public GameObject GameObject => gameObject;
        
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
    }
}
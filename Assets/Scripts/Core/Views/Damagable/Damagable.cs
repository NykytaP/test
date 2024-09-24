using System;
using Core.Services.Health;
using UnityEngine;
using Zenject;

namespace Core.Views.Damagable
{
    public class Damagable : MonoBehaviour, IDamagable
    {
        private IHealthService _healthService;

        [Inject]
        public void Construct(IHealthService healthService)
        {
            _healthService = healthService;
        }

        public void ForceKill()
        {
            _healthService.ForceKill(OnDeathCallback);
        }

        public void ResetHealth()
        {
            _healthService.ResetHealth();
        }

        public void ReceiveDamage(int damage)
        {
            _healthService.ReceiveDamage(damage, OnDeathCallback);
        }

        public Action OnDeathCallback { get; set; }
    }
}
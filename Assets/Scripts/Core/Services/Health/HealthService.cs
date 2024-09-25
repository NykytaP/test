using System;
using Core.Utils;

namespace Core.Services.Health
{
    public class HealthService : IHealthService
    {
        private int _currentHealth;

        public HealthService()
        {
            ResetHealth();
        }
        
        public void ResetHealth()
        {
            _currentHealth = Constants.GameConstants.MaxEntityHealth;
        }

        public void ForceKill(Action onDeathCallback)
        {
            ReceiveDamage(_currentHealth, onDeathCallback);
        }

        public bool IsAlive()
        {
            return _currentHealth > 0;
        }

        public void ReceiveDamage(int damage, Action onDeathCallback)
        {
            if(IsDead())
                return;

            _currentHealth -= damage;

            if (IsDead())
                onDeathCallback?.Invoke();
        }

        private bool IsDead()
        {
            return _currentHealth <= 0;
        }
    }
}
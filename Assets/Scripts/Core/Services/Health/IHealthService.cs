using System;

namespace Core.Services.Health
{
    public interface IHealthService
    {
        public void ResetHealth();
        public void ReceiveDamage(int damage, Action onDeathCallback);
        public void ForceKill(Action onDeathCallback);
        public bool IsAlive();
    }
}
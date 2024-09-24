using System;

namespace Core.Views.Damagable
{
    public interface IDamagable
    {
        public Action OnDeathCallback { get; set; }
        public void ReceiveDamage(int damage);
        public void ForceKill();
        public void ResetHealth();
    }
}
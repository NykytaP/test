using Core.Views.Damagable;
using Core.Views.TankDeath;
using Core.Views.VFX;
using UnityEngine;

namespace Core.Views.Tank
{
    public interface ITankViewContainer
    {
        public TankMoveVFXView TankMoveVFXView { get; }
        public ITankDeathView TankDeathView { get; }
        public Rigidbody Rigidbody { get; }
        public BoxCollider BoxCollider { get; }
        public IDamagable Damagable { get; }
        public Transform ShotPoint { get; }
        public Transform Entity { get; }
    }
}
using Core.Views.Damagable;
using Core.Views.TankDeath;
using Core.Views.VFX;
using UnityEngine;

namespace Core.Views.Tank
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class TankViewContainer : MonoBehaviour, ITankViewContainer
    {
        [SerializeField]
        private Rigidbody _rb;
        [SerializeField]
        private Transform _shotPoint;
        [SerializeField]
        private BoxCollider _boxCollider;
        [SerializeField]
        private Damagable.Damagable _damagable;
        [SerializeField]
        private TankDeathView _tankDeathView;
        [SerializeField]
        private TankMoveVFXView _tankMoveVFXView;

        public TankMoveVFXView TankMoveVFXView => _tankMoveVFXView;
        public ITankDeathView TankDeathView => _tankDeathView;
        public Rigidbody Rigidbody => _rb;
        public BoxCollider BoxCollider => _boxCollider;
        public IDamagable Damagable => _damagable;
        public Transform ShotPoint => _shotPoint;
        public Transform Entity => transform;
    }
}
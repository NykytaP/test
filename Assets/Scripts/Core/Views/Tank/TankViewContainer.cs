using Core.Views.Damagable;
using Core.Views.TankDeath;
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

        public ITankDeathView TankDeathView => _tankDeathView;
        public Rigidbody Rigidbody => _rb;
        public BoxCollider BoxCollider => _boxCollider;
        public IDamagable Damagable => _damagable;
        public Transform ShotPoint => _shotPoint;
        public Transform Entity => transform;
    }
}
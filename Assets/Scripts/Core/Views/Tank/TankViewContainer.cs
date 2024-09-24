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

        public Rigidbody Rigidbody => _rb;
        public BoxCollider BoxCollider => _boxCollider;
        public Transform ShotPoint => _shotPoint;
        public Transform Entity => transform;
    }
}
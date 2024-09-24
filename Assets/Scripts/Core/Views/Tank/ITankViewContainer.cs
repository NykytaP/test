using UnityEngine;

namespace Core.Views.Tank
{
    public interface ITankViewContainer
    {
        public Rigidbody Rigidbody { get; }
        public BoxCollider BoxCollider { get; }
        public Transform ShotPoint { get; }
        public Transform Entity { get; }
    }
}
using UnityEngine;

namespace Core.Views.Damagable
{
    public class TouchDamagable : Damagable
    {
        [SerializeField]
        private LayerMask _touchableLayers;

        private void OnCollisionEnter(Collision other)
        {
            if (((1 << other.gameObject.layer) & _touchableLayers) != 0)
            {
                ForceKill();
            }
        }
    }
}
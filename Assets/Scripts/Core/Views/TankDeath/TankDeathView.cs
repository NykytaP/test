using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Views.TankDeath
{
    public class TankDeathView : MonoBehaviour, ITankDeathView
    {
        private const float BlowUpForce = 5f;
        private const float TorqueForce = 0.001f;
        private static readonly Vector3 BlowUpDirection = new(0.3f, 1, 0);
        
        [SerializeField]
        private Rigidbody _turretRb;
        [SerializeField]
        private Collider _turretCollider;
        [SerializeField]
        private ParticleSystem _fireParticle;
        
        private Vector3 _localDefaultRotation;
        private Vector3 _localDefaultPosition;

        private void Awake()
        {
            CacheDefaultValues();
        }

        public void BlowUpTurret()
        {
            SetDamagedState(true);
            ApplyForToTurret();
            SetFireVFX(true);
        }
        
        public void ResetTurret()
        {
            SetDamagedState(false);

            _turretRb.velocity = Vector3.zero;
            _turretRb.angularVelocity = Vector3.zero;
            
            _turretRb.transform.localPosition = _localDefaultPosition;
            _turretRb.transform.localEulerAngles = _localDefaultRotation;
            
            SetFireVFX(false);
        }
        
        private void CacheDefaultValues()
        {
            _localDefaultRotation = _turretRb.transform.localEulerAngles;
            _localDefaultPosition = _turretRb.transform.localPosition;
        }

        private void SetDamagedState(bool state)
        {
            _turretRb.isKinematic = !state;
            _turretCollider.enabled = state;
        }

        private void ApplyForToTurret()
        {
            _turretRb.AddForce(transform.TransformDirection(BlowUpDirection) * BlowUpForce, ForceMode.Impulse);
            _turretRb.AddTorque(Random.rotation.eulerAngles * TorqueForce, ForceMode.Impulse);
        }
        
        private void SetFireVFX(bool state)
        {
            _fireParticle.gameObject.SetActive(_fireParticle);
            
            if(state)
                _fireParticle.Play();
            else
            {
                _fireParticle.Clear();
                _fireParticle.Stop();
            }
        }
    }
}
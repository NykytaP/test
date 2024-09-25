using UnityEngine;

namespace Core.Views.VFX
{
    public class TankMoveVFXView : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem[] _moveDust;

        private bool _isPlaying;
        
        private void Start()
        {
            ResetVFX();
        }

        public void SetMoveVfx(bool play)
        {
            foreach (ParticleSystem vfx in _moveDust)
            {
                if(_isPlaying && play)
                    continue;

                if (play)
                    vfx.Play();
                else
                    vfx.Stop();
            }

            _isPlaying = play;
        }
        
        private void ResetVFX()
        {
            SetMoveVfx(false);
            
            foreach (ParticleSystem vfx in _moveDust)
                vfx.Clear();
        }
    }
}
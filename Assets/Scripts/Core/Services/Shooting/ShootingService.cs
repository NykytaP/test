using System.Threading.Tasks;
using Core.Pools.Bullets;
using Core.Pools.VFX;
using Core.Utils;
using Core.Views.Bullets;
using Core.Views.Damagable;
using Core.Views.Tank;
using Core.Views.VFX;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.CancellationTokenHelper;
using UnityEngine;

namespace Core.Services.Shooting
{
    public class ShootingService : IShootingService
    {
        private readonly ICancellationTokenHelper _cancellationTokenHelper;
        private readonly IBulletsPoolManager _bulletsPoolManager;
        private readonly IVFXPoolManager _vfxPoolManager;

        private bool _canShoot = true;

        public ShootingService(ICancellationTokenHelper cancellationTokenHelper, IBulletsPoolManager bulletsPoolManager, IVFXPoolManager vfxPoolManager)
        {
            _cancellationTokenHelper = cancellationTokenHelper;
            _bulletsPoolManager = bulletsPoolManager;
            _vfxPoolManager = vfxPoolManager;
        }
        
        public void MakeShoot(Vector3 dir, ITankViewContainer tankViewContainer)
        {
            if(!_canShoot)
                return;

            BulletView bullet = _bulletsPoolManager.GetBullet();
            
            bullet.transform.position = tankViewContainer.ShotPoint.position;
            bullet.transform.rotation = tankViewContainer.ShotPoint.rotation;
            
            bullet.GameObject.SetActive(true);
            bullet.OnCollisionEnterAction += OnCollisionEntered;

            ApplyRecoil(tankViewContainer);

            DelayShoot();
        }

        private void ApplyRecoil(ITankViewContainer tankViewContainer)
        {
            tankViewContainer.Rigidbody.AddForce(-tankViewContainer.Entity.forward * Constants.GameConstants.ShootRecoilForce, ForceMode.Impulse);
        }

        private async UniTask<AsyncUnit> DelayShoot()
        {
            _canShoot = false;
            
            await Task.Delay((int)(Constants.GameConstants.ShootDelay * 1000), cancellationToken: _cancellationTokenHelper.GetSceneCancellationToken());
            
            _canShoot = true;
            
            return AsyncUnit.Default;
        }

        private void OnCollisionEntered(Collision collision, Quaternion rotation)
        {
            SetDamage(collision);
            PlayVFX(collision, rotation);
        }

        private void SetDamage(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamagable damagable))
            {
                damagable.ReceiveDamage(Constants.GameConstants.DefaultBulletDamage);
            }
        }

        private void PlayVFX(Collision collision, Quaternion rotation)
        {
            ParticleView hitVFX = _vfxPoolManager.GetHitVFX();

            hitVFX.gameObject.SetActive(true);
            hitVFX.transform.position = collision.GetContact(0).point;
            hitVFX.transform.rotation = rotation;
            
            hitVFX.PlayVFX();
        }
    }
}
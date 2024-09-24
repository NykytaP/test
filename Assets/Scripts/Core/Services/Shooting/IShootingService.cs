using Core.Views.Tank;
using UnityEngine;

namespace Core.Services.Shooting
{
    public interface IShootingService
    {
        public void MakeShoot(Vector3 dir, ITankViewContainer tankViewContainer);
    }
}
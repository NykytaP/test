using Core.Views.Tank;
using Zenject;

namespace Core.Services.Shooting.Player
{
    public interface IPlayerShootingService : ITickable
    {
        public void Initialize(ITankViewContainer tankViewContainer);
    }
}
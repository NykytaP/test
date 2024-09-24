using Core.Services.TickableRunner;
using Core.Views.Tank;

namespace Core.Services.Shooting.Player
{
    public class PlayerShootingService : IPlayerShootingService
    {
        private readonly ITickableRunner _tickableRunner;
        private readonly IShootingService _shootingService;

        private ITankViewContainer _tankViewContainer;

        public PlayerShootingService(ITickableRunner tickableRunner, IShootingService shootingService)
        {
            _tickableRunner = tickableRunner;
            _shootingService = shootingService;

            _tickableRunner.RegisterTickable(this);
        }

        public void Initialize(ITankViewContainer tankViewContainer)
        {
            _tankViewContainer = tankViewContainer;
        }
        
        public void Tick()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _shootingService.MakeShoot(_tankViewContainer.Entity.transform.forward, _tankViewContainer);
            }
        }

        ~PlayerShootingService()
        {
            _tickableRunner.UnRegisterTickable(this);
        }
    }
}
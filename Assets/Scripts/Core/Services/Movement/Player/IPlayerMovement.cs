using Core.Views.Tank;
using Zenject;

namespace Core.Services.Movement.Player
{
    public interface IPlayerMovement : IFixedTickable
    {
        public void Initialize(ITankViewContainer tankViewContainer);
        public bool BlockInput { get; set; }
    }
}
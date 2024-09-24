using Core.Views.Tank;

namespace Core.Services.Movement
{
    public interface ITankMovement
    {
        public void Initialize(ITankViewContainer tankViewContainer);
        public void RotateTank(float turnInput);
        public void MoveTank(float moveInput);
    }
}
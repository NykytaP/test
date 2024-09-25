using Core.Views.Tank;

namespace Core.Services.Movement
{
    public interface ITankMovement
    {
        public void Initialize(ITankViewContainer tankViewContainer);
        public void RotateTank(float turnInput, float speed);
        public void MoveTank(float moveInput, float speed);
    }
}
using Core.Views.Tank;

namespace Core.Services.Movement.Bot
{
    public interface IBotMovement
    {
        public void Initialize(ITankViewContainer tankViewContainer);
    }
}
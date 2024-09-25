using Core.StateMachine.BaseStateMachine.States;
using Core.Utils;

namespace Core.Services.Movement.Bot.MoveStates
{
    public class LazyRotateState : IFixedUpdatableState
    {
        private readonly ITankMovement _tankMovement;

        public LazyRotateState(ITankMovement tankMovement)
        {
            _tankMovement = tankMovement;
        }

        public void FixedUpdate()
        {
            _tankMovement.RotateTank(1, Constants.GameConstants.BotTankTurnSpeed);
        }
    }
}
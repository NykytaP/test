using System;
using System.Threading;
using System.Threading.Tasks;
using Core.StateMachine.BaseStateMachine.States;
using Core.Utils;
using Random = UnityEngine.Random;

namespace Core.Services.Movement.Bot.MoveStates
{
    public class StraightMoveWithRotations : IFixedUpdatableState, IExitState, IEnterState
    {
        private const float RandomRotateDelay = 1;
        private const float RandomRotateMinDuration = 1f;
        private const float RandomRotateMaxDuration = 5;
        
        private readonly ITankMovement _tankMovement;

        private CancellationTokenSource _cancellationTokenSource;
        private float _turnValue;
        
        public StraightMoveWithRotations(ITankMovement tankMovement)
        {
            _tankMovement = tankMovement;
        }

        public void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _turnValue = 0;
            RandomizeRotate();
        }

        public void FixedUpdate()
        {
            _tankMovement.MoveTank(1, Constants.GameConstants.BotTankMoveSpeed);
            _tankMovement.RotateTank(_turnValue, Constants.GameConstants.BotTankTurnSpeed);
        }

        public void Exit()
        {
            _tankMovement.MoveTank(0, Constants.GameConstants.BotTankMoveSpeed);
            _cancellationTokenSource.Cancel();
        }

        private async void RandomizeRotate()
        {
            try { await Task.Delay((int)(RandomRotateDelay * 1000), _cancellationTokenSource.Token); }
            catch (Exception e) { }
            
            if(_cancellationTokenSource.IsCancellationRequested)
                return;
            
            GenerateRandomRotateInput();

            float rotateDuration = GetRandomRotateDuration();
            
            try { await Task.Delay((int)(rotateDuration * 1000), _cancellationTokenSource.Token); }
            catch (Exception e) { }
            
            if(_cancellationTokenSource.IsCancellationRequested)
                return;

            _turnValue = 0;
        }

        private void GenerateRandomRotateInput()
        {
            int randomValue = Random.Range(0, 2);
            _turnValue = randomValue == 0 ? -1 : 1;
        }

        private float GetRandomRotateDuration() => Random.Range(RandomRotateMinDuration, RandomRotateMaxDuration);
    }
}
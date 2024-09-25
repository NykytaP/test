using System;
using System.Threading;
using Core.Services.Movement.Bot.MoveStates;
using Core.Services.TickableRunner;
using Core.StateMachine.BaseStateMachine;
using Core.StateMachine.BaseStateMachine.States;
using Core.Views.Tank;
using Infrastructure.Helpers.CancellationTokenHelper;
using UnityEngine;
using Zenject;
using Task = System.Threading.Tasks.Task;

namespace Core.Services.Movement.Bot
{
    public class BotMovement : IBotMovement, IFixedTickable
    {
        public static readonly string[] ObstacleLayerNames = {"Bot", "Player", "Default"};
        private const float CheckObstacleDistance = 1.8f;
        private const float SecondsDelayBtwStates = 0.5f;
        
        private readonly ITankMovement _tankMovement;
        private readonly ITickableRunner _tickableRunner;
        private readonly ICancellationTokenHelper _cancellationTokenHelper;
        private readonly LayerMask _layerMask;

        private StateMachine.BaseStateMachine.StateMachine _stateMachine;
        private ITankViewContainer _tankViewContainer;
        private bool _statesDelayActive;

        public BotMovement(ITankMovement tankMovement, ITickableRunner tickableRunner, ICancellationTokenHelper cancellationTokenHelper)
        {
            _tankMovement = tankMovement;
            _tickableRunner = tickableRunner;
            _cancellationTokenHelper = cancellationTokenHelper;

            _tickableRunner.RegisterTickable(this);
            _layerMask = LayerMask.GetMask(ObstacleLayerNames);

            _stateMachine = new StateMachine.BaseStateMachine.StateMachine(new IState[] { new StraightMoveWithRotations(_tankMovement), new LazyRotateState(_tankMovement), new AFKState() },
                new []
                {
                    new Transition(typeof(StraightMoveWithRotations), typeof(LazyRotateState), () => LazyRotateConditionCheck() && !_statesDelayActive),
                    new Transition(typeof(LazyRotateState), typeof(StraightMoveWithRotations), () => StraightMoveConditionCheck() && !_statesDelayActive),
                    new Transition(typeof(StraightMoveWithRotations), typeof(AFKState), IsDead),
                    new Transition(typeof(LazyRotateState), typeof(AFKState), IsDead),
                    new Transition(typeof(AFKState), typeof(StraightMoveWithRotations), () => !IsDead() && StraightMoveConditionCheck()),
                    new Transition(typeof(AFKState), typeof(LazyRotateState), () => !IsDead() && LazyRotateConditionCheck())
                }, SwitchDelay);
        }

        public void Initialize(ITankViewContainer tankViewContainer)
        {
            _tankViewContainer = tankViewContainer;
            _tankMovement.Initialize(tankViewContainer);
        }

        public void FixedTick()
        {
            if (_tankViewContainer == null)
                return;
            
            _stateMachine.FixedUpdate();
        }

        private bool LazyRotateConditionCheck()
        {
            return CheckObstacle();
        }

        private bool StraightMoveConditionCheck()
        {
            return !CheckObstacle();
        }

        private bool IsDead()
        {
            return !_tankViewContainer.Damagable.IsAlive();
        }

        private bool CheckObstacle()
        {
            Vector3 boxCenter = _tankViewContainer.BoxCollider.bounds.center;
            Vector3 boxHalfExtents = _tankViewContainer.BoxCollider.size / 2f;
            Vector3 forwardDirection = _tankViewContainer.Entity.forward;

            bool isHit = Physics.BoxCast(boxCenter, boxHalfExtents, forwardDirection, out RaycastHit hitInfo, _tankViewContainer.Entity.rotation, CheckObstacleDistance, _layerMask);
            
            return isHit;
        }

        private async void SwitchDelay()
        {
            CancellationToken cancellationToken = _cancellationTokenHelper.GetSceneCancellationToken();
            _statesDelayActive = true;

            try { await Task.Delay((int)(SecondsDelayBtwStates * 1000), cancellationToken: cancellationToken); }
            catch (Exception e) { }

            if (cancellationToken.IsCancellationRequested)
                return;
            
            _statesDelayActive = false;
        }

        ~BotMovement()
        {
            _tickableRunner.UnRegisterTickable(this);
        }
    }
}
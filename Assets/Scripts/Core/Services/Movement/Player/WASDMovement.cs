using Core.Services.TickableRunner;
using Core.Views.Tank;
using UnityEngine;
using Zenject;

namespace Core.Services.Movement.Player
{
    public class WASDMovement : IPlayerMovement
    {
        private readonly ITankMovement _tankMovement;
        private readonly ITickableRunner _tickableRunner;

        private float  _moveInput;
        private float  _turnInput;

        public WASDMovement(ITankMovement tankMovement, ITickableRunner tickableRunner)
        {
            _tankMovement = tankMovement;
            _tickableRunner = tickableRunner;

            _tickableRunner.RegisterTickable(this);
        }
        
        public void Initialize(ITankViewContainer tankViewContainer)
        {
            _tankMovement.Initialize(tankViewContainer);
        }
        
        public void FixedTick()
        {
            ReadInput();

            if (_moveInput != 0)
                _tankMovement.MoveTank(_moveInput);
            
            if(_turnInput != 0)
                _tankMovement.RotateTank(_turnInput);
        }

        private void ReadInput()
        {
            _moveInput = Input.GetAxisRaw("Vertical");
            _turnInput = Input.GetAxisRaw("Horizontal");
        }

        ~WASDMovement()
        {
            _tickableRunner.UnRegisterTickable(this);
        }
    }
}
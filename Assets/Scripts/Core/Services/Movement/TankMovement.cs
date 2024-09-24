using Core.Utils;
using Core.Views.Tank;
using UnityEngine;

namespace Core.Services.Movement
{
    public class TankMovement : ITankMovement
    {
        private ITankViewContainer _tankViewContainer;

        public void Initialize(ITankViewContainer tankViewContainer)
        {
            _tankViewContainer = tankViewContainer;
        }

        public void MoveTank(float moveInput)
        {
            if(_tankViewContainer == null)
                return;
            
            Vector3 movement = _tankViewContainer.Rigidbody.transform.forward * moveInput * Constants.GameConstants.TankMoveSpeed * Time.fixedDeltaTime;
            _tankViewContainer.Rigidbody.MovePosition(_tankViewContainer.Rigidbody.position + movement);
        }

        public void RotateTank(float turnInput)
        {
            if(_tankViewContainer == null)
                return;
            
            float turn = turnInput * Constants.GameConstants.TankTurnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            
            _tankViewContainer.Rigidbody.MoveRotation(_tankViewContainer.Rigidbody.rotation * turnRotation);
        }
    }
}
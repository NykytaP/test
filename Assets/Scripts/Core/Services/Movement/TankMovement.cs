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

        public void MoveTank(float moveInput, float speed)
        {
            if(_tankViewContainer == null)
                return;
            
            Vector3 movement = _tankViewContainer.Rigidbody.transform.forward * moveInput * speed * Time.fixedDeltaTime;
            _tankViewContainer.Rigidbody.MovePosition(_tankViewContainer.Rigidbody.position + movement);
            
            _tankViewContainer.TankMoveVFXView.SetMoveVfx(Mathf.Approximately(moveInput, 1));
        }

        public void RotateTank(float turnInput, float speed)
        {
            if(_tankViewContainer == null)
                return;
            
            float turn = turnInput * speed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            
            _tankViewContainer.Rigidbody.MoveRotation(_tankViewContainer.Rigidbody.rotation * turnRotation);
        }
    }
}
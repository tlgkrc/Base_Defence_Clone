using Data.ValueObject;
using DG.Tweening;
using Enums;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables
        
        #region Serialized Variables
        
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private PlayerManager manager;
        
        #endregion
        
        #region Private Variables
        
        private PlayerMovementData _movementData;
        private bool _isReadyToMove, _isReadyToPlay = false;
        private float _inputValueX;
        private float _inputValueZ;

        #endregion
        
        #endregion

        public void SetMovementData(PlayerMovementData dataMovementData)
        {
            _movementData = dataMovementData;
        }

        public void EnableMovement()
        {
            _isReadyToMove = true;
        }

        public void DisableMovement()
        {
            _isReadyToMove = false;
        }

        public void UpdateIdleInputValue(IdleInputParams inputParams)
        {
            _inputValueX = inputParams.ValueX;
            _inputValueZ = inputParams.ValueZ;
        }
        

        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }
        
        private void FixedUpdate()
        {
            if (!_isReadyToPlay) return;
            if (_isReadyToMove)
            {
                IdleMove();

            }
            else
            {
                Stop();
            }
        }

        private void IdleMove()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValueX * _movementData.Speed, velocity.y,
                _inputValueZ*_movementData.Speed);
            rigidbody.velocity = velocity;

            var position1 = rigidbody.position;
            var position = new Vector3(position1.x, position1.y, position1.z);
            position1 = position;
            rigidbody.position = position1;
            
            if (velocity != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(velocity, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,
                                _movementData.IdleRotateSpeed*Time.fixedDeltaTime);
            }
            
        }

        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}
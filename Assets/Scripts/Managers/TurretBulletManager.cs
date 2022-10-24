using System;
using Controllers.Turret;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class TurretBulletManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private TurretBulletPhysicController physicController;

        #endregion

        #region Private Variables

        private float _lifeTime;
        private bool _isActive;

        #endregion

        #endregion

        private void BulletMove()
        {
            rigidbody.AddRelativeForce(Vector3.forward*8,ForceMode.VelocityChange);
        }

        private void FixedUpdate()
        {
            if (_isActive)
            {
                BulletMove();
                _lifeTime += Time.fixedDeltaTime;
                if (_lifeTime>= 4f)
                {
                    ResetBullet();
                    PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolTypes.Bullet.ToString(),gameObject);
                }
            }
            
        }

        #region Subscriptions Events

        private void OnEnable()
        {
            _isActive = true;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }

        private void UnsubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            _isActive = false;
            UnsubscribeEvents();
        }

        #endregion
        
        public void ResetBullet()
        {
            rigidbody.velocity = Vector3.zero;
            _lifeTime = 0f;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        
    }
}
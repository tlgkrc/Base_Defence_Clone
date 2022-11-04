using Controllers.Turret;
using Data.UnityObject;
using Data.ValueObject.Weapon;
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
        private TurretBulletData _turretBulletData;

        #endregion

        #endregion

        private void Awake()
        {
            _turretBulletData = GetBulletData();
        }

        private TurretBulletData GetBulletData()
        {
            return Resources.Load<CD_BulletData>("Data/CD_BulletData").BulletData.BulletGoData.TurretBulletData;
        }

        private void BulletMove()
        {
            rigidbody.AddRelativeForce(Vector3.forward*_turretBulletData.ForceFactor,ForceMode.VelocityChange);
        }

        private void FixedUpdate()
        {
            if (_isActive)
            {
                BulletMove();
                _lifeTime += Time.fixedDeltaTime;
                if (_lifeTime>= _turretBulletData.LifeTime)
                {
                    ResetBullet();
                    PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolTypes.TurretBullet.ToString(),gameObject);
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
            CoreGameSignals.Instance.onSetTurretBulletDamage += OnSetTurretBulletDamage;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onSetTurretBulletDamage -= OnSetTurretBulletDamage;

        }

        private void OnDisable()
        {
            _isActive = false;
            UnsubscribeEvents();
        }

        #endregion

        private int OnSetTurretBulletDamage()
        {
            return _turretBulletData.Damage;
        }
        
        public void ResetBullet()
        {
            rigidbody.velocity = Vector3.zero;
            _lifeTime = 0f;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
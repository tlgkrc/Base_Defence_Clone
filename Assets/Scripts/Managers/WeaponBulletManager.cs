using Controllers;
using Data.UnityObject;
using Data.ValueObject.Weapon;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class WeaponBulletManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private WeaponBulletPhysicController physicController;
        [SerializeField] private WeaponTypes weaponTypes;

        #endregion

        #region Private Variables

        private float _lifeTime;
        private bool _isActive;
        private WeaponBulletGOData _weaponBulletGoData;

        #endregion

        #endregion

        private void Awake()
        {
            _weaponBulletGoData = GetBulletData();
        }

        #region Subscriptions Events

        private void OnEnable()
        {
            _isActive = true;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onSetWeaponBulletDamage += OnSetWeaponBulletDamage;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onSetWeaponBulletDamage -= OnSetWeaponBulletDamage;

        }

        private void OnDisable()
        {
            _isActive = false;
            UnsubscribeEvents();
        }

        #endregion

        private WeaponBulletGOData GetBulletData()
        {
            return Resources.Load<CD_BulletData>("Data/CD_BulletData").BulletData.BulletGoData.WeaponBulletData.WeaponBulletGODatas[weaponTypes];
        }

        private int OnSetWeaponBulletDamage()
        {
            return _weaponBulletGoData.Damage;
        }
        
        private void BulletMove()
        {
            rigidbody.AddRelativeForce(Vector3.forward*_weaponBulletGoData.ForceFactor,ForceMode.VelocityChange);
        }
        
        private void FixedUpdate()
        {
            if (_isActive)
            {
                BulletMove();
                _lifeTime += Time.fixedDeltaTime;
                if (_lifeTime>= _weaponBulletGoData.LifeTime)
                {
                    ResetBullet();
                }
            }
        }
        
        public void ResetBullet()
        {
            rigidbody.velocity = Vector3.zero;
            _lifeTime = 0f;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            PoolSignals.Instance.onReleasePoolObject?.Invoke(weaponTypes.ToString(),gameObject);
        }
    }
}
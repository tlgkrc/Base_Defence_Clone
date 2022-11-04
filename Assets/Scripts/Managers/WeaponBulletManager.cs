using Controllers;
using Data.UnityObject;
using Data.ValueObject.Weapon;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(Rigidbody))]
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
            physicController.SetWeaponType(weaponTypes);
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
            UnsubscribeEvents();
            _isActive = false;
        }

        #endregion

        private WeaponBulletGOData GetBulletData()
        {
            return Resources.Load<CD_BulletData>("Data/CD_BulletData").BulletData.BulletGoData.WeaponBulletData.WeaponBulletGODatas[weaponTypes];
        }

        private int OnSetWeaponBulletDamage(WeaponTypes weaponType)
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
                    PoolSignals.Instance.onReleasePoolObject?.Invoke(weaponTypes.ToString(),gameObject);
                }
            }
        }
        
        public void ResetBullet()
        {
            rigidbody.velocity = Vector3.zero;
            _lifeTime = 0f;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        public int GetWeaponDamage()
        {
            return _weaponBulletGoData.Damage;
        }
    }
}
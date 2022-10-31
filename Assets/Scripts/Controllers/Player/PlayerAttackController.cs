using System.Collections.Generic;
using Data.ValueObject.Weapon;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers.Player
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerAttackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables
        
        
        #endregion

        #region Private Variables

        [ShowInInspector] private List<GameObject> _enemies = new List<GameObject>();
        private WeaponTypes _weaponType;
        private WeaponData _weaponData;
        private float _shootTime;

        #endregion
        #endregion
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _enemies.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _enemies.Remove(other.gameObject);
                _enemies.TrimExcess();
            }
        }

        private void Update()
        {
            if (_enemies.Count<=0)
            {
                return;
            }
            Shoot();
        }

        private void Shoot()
        {
            AutoShoot();
        }
        
        private void AutoShoot()
        {
            _shootTime += Time.deltaTime;
            while (_shootTime >= _weaponData.WeaponDatas[_weaponType].ShootingDelay)
            {
                _shootTime = 0;
                var gO = PoolSignals.Instance.onGetPoolObject(_weaponType.ToString(), transform);
                gO.transform.localRotation = transform.rotation;
            }
        }

        public void SetWeaponData(WeaponData weaponData)
        {
            _weaponData = weaponData;
        }
        public void SetHoldWeapon(WeaponTypes weaponType)
        {
            _weaponType = weaponType;
        }
    }
}
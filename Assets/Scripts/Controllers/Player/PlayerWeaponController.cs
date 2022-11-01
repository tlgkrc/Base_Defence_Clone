using System.Collections.Generic;
using Data.ValueObject.Weapon;
using Enums;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers.Player
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerWeaponController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private List<GameObject> weaponList;
        [SerializeField] private List<Transform> muzzleList;

        #endregion

        #region Private Variables

        [ShowInInspector] private List<GameObject> _enemies = new List<GameObject>();
        private WeaponTypes _weaponType;
        private WeaponData _weaponData;
        private float _shootTime;
        private int _currentWeaponIndex;

        #endregion
        
        #endregion
        public void SetWeaponData(WeaponData weaponData)
        {
            _weaponData = weaponData;
            SetDefaultProperties();
        }

        private void SetDefaultProperties()
        {
            foreach (var value in weaponList)
            {
                value.SetActive(false);
            }
            _currentWeaponIndex = 0;
        }

        public void TakeHandWeapon(WeaponTypes weaponTypes)
        {
            _currentWeaponIndex = (int)weaponTypes;
            _weaponType = weaponTypes;
        }

        public void SetWeaponVisual(bool isOpen)
        {
            weaponList[_currentWeaponIndex].SetActive(isOpen);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                CoreGameSignals.Instance.onCheckCloseEnemy?.Invoke();
                _enemies.Add(other.transform.parent.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _enemies.Remove(other.transform.parent.gameObject);
                _enemies.TrimExcess();
                CoreGameSignals.Instance.onCheckCloseEnemy?.Invoke();
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
                var gO = PoolSignals.Instance.onGetPoolObject(_weaponType.ToString(), muzzleList[_currentWeaponIndex]);
                gO.transform.localRotation = transform.rotation;
            }
        }

        public GameObject CheckTarget()
        {
            if (_enemies.Count>=1)
            {
                return _enemies[0];
            }
            else
            {
                return null;
            }
        }

        public void EnemyDie(GameObject enemy)
        {
            _enemies.Remove(enemy);
            _enemies.TrimExcess();
            CoreGameSignals.Instance.onCheckCloseEnemy?.Invoke();
        }
    }
}
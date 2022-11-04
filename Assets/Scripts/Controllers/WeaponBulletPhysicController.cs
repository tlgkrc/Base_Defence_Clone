using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class WeaponBulletPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private WeaponBulletManager manager;

        #endregion

        #region Private Variables

        private WeaponTypes _weaponTypes;

        #endregion

        #endregion

        public void SetWeaponType(WeaponTypes weaponTypes)
        {
            _weaponTypes = weaponTypes;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                manager.ResetBullet();
                PoolSignals.Instance.onReleasePoolObject?.Invoke(_weaponTypes.ToString(),manager.gameObject);
            }
        }
    }
}
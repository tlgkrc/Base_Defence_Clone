using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class WeaponBulletPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private WeaponBulletManager manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                manager.ResetBullet();
            }
        }
    }
}
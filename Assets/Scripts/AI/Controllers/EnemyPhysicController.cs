using System;
using AI.Subscribers;
using UnityEngine;

namespace AI.Controllers
{
    public class EnemyPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Enemy manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("TurretBullet"))
            {
                manager.Hit(false);
            }
            else if (other.CompareTag("WeaponBullet"))
            {
                manager.Hit(true);
            }
        }
    }
}
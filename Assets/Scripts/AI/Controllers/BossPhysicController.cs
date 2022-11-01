using System;
using AI.Subscribers;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace AI.Controllers
{
    public class BossPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Boss manager;

        #endregion

        #region Private Variables



        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("WeaponBullet"))
            {
                manager.Hit(other.transform.parent.GetComponent<WeaponBulletManager>().GetWeaponDamage());
            }
        }
    }
}
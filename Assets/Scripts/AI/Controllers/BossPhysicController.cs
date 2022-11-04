using AI.Subscribers;
using Managers;
using UnityEngine;

namespace AI.Controllers
{
    public class BossPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Boss manager;

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
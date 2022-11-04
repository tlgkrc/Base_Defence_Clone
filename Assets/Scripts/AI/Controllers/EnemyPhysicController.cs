using AI.Subscribers;
using Managers;
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
                manager.Hit(false,0);
            }
            else if (other.CompareTag("WeaponBullet"))
            {
                manager.Hit(true,other.transform.parent.GetComponent<WeaponBulletManager>().GetWeaponDamage());
            }
            else if (other.CompareTag("Player"))
            {
                manager.AttackPlayer();
            }
        }
    }
}
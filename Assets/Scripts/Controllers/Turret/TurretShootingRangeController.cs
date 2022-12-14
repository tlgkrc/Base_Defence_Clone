using Managers;
using UnityEngine;

namespace Controllers.Turret
{
    public class TurretShootingRangeController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TurretManager turretManager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                turretManager.AddEnemyToHitList(other.transform.parent.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                turretManager.RemoveEnemyFromHitList(other.transform.parent.gameObject);
            }
        }
    }
}
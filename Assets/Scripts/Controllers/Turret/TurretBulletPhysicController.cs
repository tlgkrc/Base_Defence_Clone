using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Turret
{
    public class TurretBulletPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TurretBulletManager manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                manager.ResetBullet();
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolTypes.TurretBullet.ToString(),manager.gameObject);
            }
        }
    }
}
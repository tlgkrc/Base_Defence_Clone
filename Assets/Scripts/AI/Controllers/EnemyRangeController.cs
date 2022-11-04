using AI.Subscribers;
using UnityEngine;

namespace AI.Controllers
{
    [RequireComponent(typeof(Collider))]
    public class EnemyRangeController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Enemy manager;

        #endregion

        #endregion
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.SetPlayerRange(true);
                manager.Target = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.SetPlayerRange(false);
                manager.Target = null;
            }
        }
    }
}
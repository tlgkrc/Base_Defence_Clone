using AI.Subscribers;
using Signals;
using UnityEngine;

namespace AI.Controllers
{
    public class HostagePhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private Hostage manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StackSignals.Instance.onAddHostageToStack?.Invoke(transform.parent.gameObject);
            }
            else if (other.CompareTag("MineCounter"))
            {
                manager.gameObject.SetActive(false);
            }
        }
    }
}
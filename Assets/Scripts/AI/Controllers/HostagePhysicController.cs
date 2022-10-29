using AI.Subscribers;
using Enums;
using Enums.Animations;
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
                manager.SetAnim(HostageAnimState.Walk);
                manager.SetState(true);
            }
            else if (other.CompareTag("MineCounter"))
            {
                StackSignals.Instance.onRemoveHostageFromStack?.Invoke(manager.gameObject);
                manager.ResetHostage();
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolTypes.Hostage.ToString(),manager.gameObject);
                BaseSignals.Instance.onAddMiner?.Invoke();
            }
        }
    }
}
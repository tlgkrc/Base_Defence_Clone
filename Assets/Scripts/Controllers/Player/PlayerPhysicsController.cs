using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private StackManager stackManager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MineStack"))
            {
                StackSignals.Instance.onClearStaticStack?.Invoke(transform);
            }
            else if (other.CompareTag("Hostage"))
            {
                StackSignals.Instance.onAddStack?.Invoke(transform.parent.GetInstanceID());
            }
            else if(other.CompareTag("Money"))
            {
                StackSignals.Instance.onAddStack?.Invoke(transform.parent.GetInstanceID());
            }
            else if (other.CompareTag("Gate") && stackManager.transform.childCount > 0)
            {
                StackSignals.Instance.onClearDynamicStack?.Invoke(manager.transform.GetInstanceID());
            }
        }
    }
}
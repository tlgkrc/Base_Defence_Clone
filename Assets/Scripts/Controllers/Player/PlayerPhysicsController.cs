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

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MineStack"))
            {
                StackSignals.Instance.onClearStack?.Invoke(transform);
            }
            else if (other.CompareTag("Hostage"))
            {
                StackSignals.Instance.onAddStack?.Invoke();
            }
            else if(other.CompareTag("Money"))
            {
                StackSignals.Instance.onAddStack?.Invoke();
            }
        }
    }
}
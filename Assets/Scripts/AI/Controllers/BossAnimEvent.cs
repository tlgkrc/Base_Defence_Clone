using Signals;
using UnityEngine;

namespace AI.Controllers
{
    public class BossAnimEvent : MonoBehaviour
    {
        public void OnTriggerThrowEvent()
        {
            BaseSignals.Instance.onTriggerThrowEvent?.Invoke();
        }

        public void OnTriggerFakeHoldEvent()
        {
            BaseSignals.Instance.onTriggerFakeHoldEvent?.Invoke();
        }
    }
}
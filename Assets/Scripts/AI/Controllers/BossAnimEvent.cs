using Signals;
using UnityEngine;

namespace AI.Controllers
{
    public class BossAnimEvent : MonoBehaviour
    {
        public void OnTriggerThrowEvent()
        {
            AISignals.Instance.onTriggerThrowEvent?.Invoke();
        }

        public void OnTriggerFakeHoldEvent()
        {
            AISignals.Instance.onTriggerFakeHoldEvent?.Invoke();
        }
    }
}
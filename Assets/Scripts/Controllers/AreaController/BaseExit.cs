using System;
using Signals;
using UnityEngine;

namespace Controllers.AreaController
{
    public class BaseExit : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                BaseSignals.Instance.onPlayerInBase?.Invoke(false);
            }
        }
    }
}
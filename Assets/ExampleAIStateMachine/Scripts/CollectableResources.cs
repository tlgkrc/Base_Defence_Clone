using System;
using UnityEngine;
using UnityEngine.AI;

namespace ExampleAIStateMachine.Scripts
{
    public class CollectableResources : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool IsDepleted => _available <= 0;

        #endregion

        #region Serialized Variables

        [SerializeField] private int _totalAvailable = 1;

        #endregion

        #region Private Variables

        private int _available;

        #endregion

        #endregion

        private void OnEnable()
        {
            _available = _totalAvailable;
        }

        public bool Take()
        {
            if (_available <= 0)
            {
                return false;
            }
            _available--;
            UpdateSize();
            return true;
        }

        private void UpdateSize()
        {
            var scale = (float)_available / _totalAvailable;
            if (scale > 0 && scale < 1f)
            {
                var vectorScale = Vector3.one * scale;
                transform.localScale = vectorScale;
            }
            else if (scale <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        [ContextMenu("Snap")]
        private void Snap()
        {
            if (NavMesh.SamplePosition(transform.position, out var hit, 5f, NavMesh.AllAreas))
            {
                transform.position = hit.position;
                Debug.Log("taken resource");
            }
        }

        public void SetAvailable(int amount) => _available = amount;
    }
}
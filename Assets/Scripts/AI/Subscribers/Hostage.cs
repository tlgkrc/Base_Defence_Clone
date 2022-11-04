using AI.Controllers;
using Enums.Animations;
using Signals;
using UnityEngine;

namespace AI.Subscribers
{
    public class Hostage : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private GameObject helpGameObject;
        [SerializeField] private HostageAnimController animController;

        #endregion

        #region Private Variables

        private bool _isCollected;

        #endregion

        #endregion

        private void Awake()
        {
            SetAnim(HostageAnimState.Sit);
        }
        #region Subscription Events

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onAddHostageToStack += OnAddHostageToStack;
            StackSignals.Instance.onActivateMoveHostageStack += OnActivateMoveHostageStack;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddHostageToStack -= OnAddHostageToStack;
            StackSignals.Instance.onActivateMoveHostageStack -= OnActivateMoveHostageStack;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public void SetAnim(HostageAnimState animState)
        {
            animController.SetAnim(animState);
        }

        public void SetState(bool isCollected)
        {
            _isCollected = true;
            helpGameObject.SetActive(false);
        }
        
        private void OnAddHostageToStack(GameObject arg0)
        {
            SetAnim(HostageAnimState.Idle);
        }
        
        private void OnActivateMoveHostageStack(bool arg0)
        {
            if (_isCollected == false)
            {
                return;
            }
            if (arg0)
            {
                SetAnim(HostageAnimState.Walk);
            }
            else
            {
                SetAnim(HostageAnimState.Idle);
            }
        }

        public void ResetHostage()
        {
            helpGameObject.SetActive(true);
            _isCollected = false;
            SetAnim(HostageAnimState.Sit);
        }
    }
}
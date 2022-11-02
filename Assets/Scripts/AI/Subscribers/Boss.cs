using System;
using AI.Controllers;
using Enums.Animations;
using Signals;
using UnityEngine;

namespace AI.Subscribers
{
    public class Boss : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BossPhysicController physicController;
        [SerializeField] private BossAnimController animController;
        [SerializeField] private BossHealthController healthController;
        [SerializeField] private BossAttackController attackController;
       

        #endregion

        #region Private Variables

        private GameObject _target;

        //Data//
        private int _health = 70;
        
        #endregion

        #endregion

        private void Awake()
        {
            SetDataToControllers();
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BaseSignals.Instance.onTriggerThrowEvent += OnTriggerThrowEvent;
            BaseSignals.Instance.onTriggerFakeHoldEvent += OnTriggerFakeHoldEvent;
        }

        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onTriggerThrowEvent -= OnTriggerThrowEvent;
            BaseSignals.Instance.onTriggerFakeHoldEvent += OnTriggerFakeHoldEvent;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void SetDataToControllers()
        {
            healthController.SetHealthData(_health);
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
            if (target == null)
            {
                animController.SetBossAnim(BossAnimTypes.Idle);
            }
            else
            {
                animController.SetBossAnim(BossAnimTypes.Throw);
            }
        }

        public void Hit(int damage)
        {
            healthController.Hit(damage);
        }

        public GameObject GetTarget()
        {
            return _target;
        }

        private void OnTriggerThrowEvent()
        {
            if (_target != null)
            {
                attackController.ThrowBomb();
            }
        }

        private void OnTriggerFakeHoldEvent()
        {
            attackController.PrepareThrow();
        }
    }
}
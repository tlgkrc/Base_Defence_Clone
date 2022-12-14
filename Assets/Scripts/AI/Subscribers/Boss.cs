using AI.Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums.Animations;
using Signals;
using UnityEngine;

namespace AI.Subscribers
{
    public class Boss : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Collider portalCollider;
        [SerializeField] private BossPhysicController physicController;
        [SerializeField] private BossAnimController animController;
        [SerializeField] private BossHealthController healthController;
        [SerializeField] private BossAttackController attackController;

        #endregion

        #region Private Variables

        private bool _isDead;
        private GameObject _target;
        private BossData _bossData;

        #endregion

        #endregion

        private void Awake()
        {
            _bossData = GetBossData();
            _isDead = false;
            SetDataToControllers();
        }

        private BossData GetBossData()
        {
            return Resources.Load<CD_BossData>("Data/CD_BossData").BossData;
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AISignals.Instance.onTriggerThrowEvent += OnTriggerThrowEvent;
            AISignals.Instance.onTriggerFakeHoldEvent += OnTriggerFakeHoldEvent;
        }

        private void UnsubscribeEvents()
        {
            AISignals.Instance.onTriggerThrowEvent -= OnTriggerThrowEvent;
            AISignals.Instance.onTriggerFakeHoldEvent -= OnTriggerFakeHoldEvent;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void SetDataToControllers()
        {
            healthController.SetHealthData(_bossData);
            attackController.SetThrowTime(_bossData.ThrowTime);
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
            if (target != null && !_isDead)
            {
                animController.SetBossAnim(BossAnimTypes.Throw);
            }

            if (target ==null)
            {
                animController.SetBossAnim(BossAnimTypes.Idle);
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

        public void OpenPortal()    
        {
            _isDead = true;
            animController.SetBossAnim(BossAnimTypes.Die);
            portalCollider.enabled = true;
            attackController.gameObject.SetActive(false); 
            physicController.gameObject.SetActive(false);
            CoreGameSignals.Instance.onDieEnemy?.Invoke(gameObject);
            Invoke(nameof(Die), 1.5f);
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}
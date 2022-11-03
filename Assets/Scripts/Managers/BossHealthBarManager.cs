using Signals;
using UnityEngine;

namespace Managers
{
    public class BossHealthBarManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private HealthBar healthBar;

        #endregion
        
        #region Private Variables

        private GameObject _boss;

        #endregion

        #endregion

        private void Start()
        {
            _boss = GameObject.Find("Boss").gameObject;
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onSetBossHealthRatio += OnSetHealthRatio;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onSetBossHealthRatio += OnSetHealthRatio;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void Update()
        {
            transform.position = _boss.transform.position + Vector3.up * 7.5f;
        }

        private void OnSetHealthRatio(float ratio)
        {
            if (ratio <= 0.1f)
            {
                healthBar.gameObject.SetActive(false);
            }
            healthBar.HealthNormalized = ratio;
        }
    }
}
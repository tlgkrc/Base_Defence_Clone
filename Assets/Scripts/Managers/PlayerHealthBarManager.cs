using System;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerHealthBarManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private HealthBar healthBar;

        #endregion
        
        #region Private Variables

        private GameObject _player;

        #endregion

        #endregion

        private void Start()
        {
            _player = GameObject.Find("PlayerManager").gameObject;
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onSetPlayerHealthRatio += OnSetHealthRatio;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onSetPlayerHealthRatio -= OnSetHealthRatio;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void Update()
        {
            transform.position = _player.transform.position + Vector3.up * 2.75f;
        }

        private void OnSetHealthRatio(float ratio)
        {
            healthBar.HealthNormalized = ratio;
        }
    }
}
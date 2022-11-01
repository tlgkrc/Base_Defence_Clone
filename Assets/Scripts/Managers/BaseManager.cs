using System;
using System.Collections.Generic;
using Controllers.AreaController;
using Data.ValueObject.Base;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshPro baseText;
        [SerializeField] private List<Transform> mineTransforms;
        [SerializeField] private List<Transform> baseTurretTransforms;
        [SerializeField] private List<Transform> ammoStockTransforms;
        [SerializeField] private Transform gemStockTransform;
        [SerializeField] private Transform ammoDepotTransform;
        [SerializeField] private BuyAmmoWorkerPhysicController buyAmmoWorkerPhysicController;
        [SerializeField] private BuyMoneyWorkerPhysicController buyMoneyWorkerPhysicController;
        
        #endregion

        #region Private Variables
        
        private const string Base = "BASE ";

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BaseSignals.Instance.onSetBaseLevelText += OnSetBaseLevelText;
            BaseSignals.Instance.onSetMineTransforms += OnSetMineTransforms;
            BaseSignals.Instance.onSetGemStock += OnSetGemStockTransform;
            BaseSignals.Instance.onSetAmmoDepotTransform += OnSetAmmoDepotTransforms;
            BaseSignals.Instance.onSetBaseTargetTransforms += OnSetBaseTargetTransforms;
            BaseSignals.Instance.onSetBaseTransform += OnSetBaseTransform;
        }

        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onSetBaseLevelText -= OnSetBaseLevelText;
            BaseSignals.Instance.onSetMineTransforms -= OnSetMineTransforms;
            BaseSignals.Instance.onSetGemStock -= OnSetGemStockTransform;
            BaseSignals.Instance.onSetAmmoDepotTransform -= OnSetAmmoDepotTransforms;
            BaseSignals.Instance.onSetBaseTargetTransforms -= OnSetBaseTargetTransforms;
            BaseSignals.Instance.onSetBaseTransform -= OnSetBaseTransform;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnSetBaseLevelText(int levelId)
        {
            baseText.text = Base + (levelId +1 ).ToString();
        }

        private List<Transform> OnSetMineTransforms()
        {
            return mineTransforms;
        }

        private Transform OnSetGemStockTransform()
        {
            return gemStockTransform;
        }

        private Transform OnSetAmmoDepotTransforms()
        {
            return ammoDepotTransform;
        }

        private List<Transform> OnSetBaseTargetTransforms()
        {
            return baseTurretTransforms;
        }

        private Transform OnSetBaseTransform()
        {
            return transform;
        }
    }
}
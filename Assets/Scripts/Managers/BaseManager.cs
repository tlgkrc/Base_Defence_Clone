using System;
using System.Collections.Generic;
using Controllers.AreaController;
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
        [SerializeField] private List<Transform> mineTransforms = new List<Transform>();
        [SerializeField] private Transform gemStockTransform;
        [SerializeField] private List<Transform> ammoStockTransforms;
        [SerializeField] private Transform ammoDepotTransform;

        #endregion

        #region Private Variables

        private List<int> _currentStages;//will be saved for each stageArea
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
            BaseSignals.Instance.onSetAmmoStockTransforms += OnSetAmmoStockTransforms;
            BaseSignals.Instance.onSetAmmoDepotTransform += OnSetAmmoDepotTransforms;
        }

        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onSetBaseLevelText -= OnSetBaseLevelText;
            BaseSignals.Instance.onSetMineTransforms -= OnSetMineTransforms;
            BaseSignals.Instance.onSetGemStock -= OnSetGemStockTransform;
            BaseSignals.Instance.onSetAmmoStockTransforms -= OnSetAmmoStockTransforms;
            BaseSignals.Instance.onSetAmmoDepotTransform -= OnSetAmmoDepotTransforms;


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
        
        private List<Transform> OnSetAmmoStockTransforms()
        {
            return ammoStockTransforms;
        }

        private Transform OnSetAmmoDepotTransforms()
        {
            return ammoDepotTransform;
        }
    }
}
using System;
using System.Collections.Generic;
using Controllers.AreaController;
using Signals;
using UnityEngine;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> StageAreaList = new List<GameObject>();
        [SerializeField] private Stage stage;

        #endregion

        #region Private Variables

        private List<int> _currentStages;//will be saved for each stageArea

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BaseSignals.Instance.onUpdateStageArea += OnUpdateStageArea;
        }

        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onUpdateStageArea -= OnUpdateStageArea;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        

        private void OnUpdateStageArea(GameObject gO)
        {
            stage.UpdateStageArea(gO);
        }
    }
}
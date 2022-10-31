using System;
using Commands;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        #endregion

        #region Private Variables
        
        private SetScoreCommand _setScoreCommand;
        private int _money, _diamond;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateMoneyScore += OnUpdateMoneyScore;
            ScoreSignals.Instance.onUpdateDiamonScore += OnUpdateDiamondScore;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateMoneyScore -= OnUpdateMoneyScore;
            ScoreSignals.Instance.onUpdateDiamonScore -= OnUpdateDiamondScore;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            UISignals.Instance.onSetDiamondText?.Invoke(_diamond);
            UISignals.Instance.onSetMoneyText?.Invoke(_money);
        }

        private void OnUpdateDiamondScore(int increase)
        {
            _diamond += increase;
            UISignals.Instance.onSetDiamondText?.Invoke(_diamond);
            Debug.Log(_diamond);
        }

        private void OnUpdateMoneyScore(int increase)
        {
            _money += increase;
            UISignals.Instance.onSetMoneyText?.Invoke(_money);
        }

    }
}
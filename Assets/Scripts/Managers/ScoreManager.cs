using System;
using System.Collections.Generic;
using Commands;
using Interfaces;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour,ISaveLoad
    {
        #region Self Variables

        #region Private Variables
        
        private SetScoreCommand _setScoreCommand;
        private int _money;
        private int _diamond;

        #endregion

        #endregion

        #region Event Subscriptions

        private void Awake()
        {
            LoadKeys();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateMoneyScore += OnUpdateMoneyScore;
            ScoreSignals.Instance.onUpdateDiamonScore += OnUpdateDiamondScore;
            ScoreSignals.Instance.onGetMoneyScore += OnGetMoneyScore;
            ScoreSignals.Instance.onGetDiamondScore += OnGetDiamondScore;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateMoneyScore -= OnUpdateMoneyScore;
            ScoreSignals.Instance.onUpdateDiamonScore -= OnUpdateDiamondScore;
            ScoreSignals.Instance.onGetMoneyScore -= OnGetMoneyScore;
            ScoreSignals.Instance.onGetDiamondScore -= OnGetDiamondScore;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
            SaveKeys();
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

        private int OnGetMoneyScore()
        {
            return _money;
        }
        private int OnGetDiamondScore()
        {
            return _diamond;
        }
        
        public void LoadKeys()
        {
            _money=SaveManager.LoadValue("Money",_money);
            _diamond=SaveManager.LoadValue("Diamond", _diamond);
        }

        public void SaveKeys()
        {
            SaveManager.SaveValue("Money",_money);
            SaveManager.SaveValue("Diamond",_diamond);
        }
    }
}
using System;
using Data.UnityObject;
using Data.ValueObject.Base;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class MineManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables



        #endregion

        #region Serialized Variables

        [SerializeField] private Transform mineSpawnTransform;
        [SerializeField] private TextMeshPro mineCountText;

        #endregion

        #region Private Variable

        private int _mineCount;
        private MineData _mineData;

        #endregion

        #endregion

        #region Subscription Events

        private void Awake()
        {
            _mineData = GetMineData();
        }

        private MineData GetMineData()
        {
            return Resources.Load<CD_BaseData>("Data/CD_BaseData").BaseData.BaseGoData.MineData;
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BaseSignals.Instance.onAddMiner += OnAddMiner;
        }

        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onAddMiner -= OnAddMiner;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnAddMiner()
        {
            if (_mineCount>=_mineData.MaxMiner)
            {
                return;
            }
            PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Miner.ToString(), mineSpawnTransform);
            _mineCount++;
            SetText();
        }

        private void SetText()
        {
            mineCountText.text = _mineCount.ToString() + "/" +_mineData.MaxMiner;
        }
    }
}
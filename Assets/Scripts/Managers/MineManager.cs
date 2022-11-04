using Data.UnityObject;
using Data.ValueObject.Base;
using Enums;
using Interfaces;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class MineManager : MonoBehaviour, ISaveLoad
    {
        #region Self Variables
        
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
            LoadKeys();
            SubscribeEvents();
        }

        private void Start()
        {
            SetText();
            Init();
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
            SaveKeys();
            UnsubscribeEvents();
        }

        #endregion
        
        private void Init()
        {
            for (int i = 0; i < _mineCount; i++)
            {
                PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Miner.ToString(), mineSpawnTransform);
            }
        }

        private void OnAddMiner(Transform newTransform)
        {
            if (_mineCount>=_mineData.MaxMiner)
            {
                return;
            }
            
            GameObject newMiner;
            newMiner = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Miner.ToString(),mineSpawnTransform );
            if (newMiner != null) newMiner.transform.position = newTransform.position;
            _mineCount++;
            SetText();
        }

        private void SetText()
        {
            mineCountText.text = _mineCount.ToString() + "/" +_mineData.MaxMiner;
        }

        public void LoadKeys()
        {
           _mineCount =  SaveManager.LoadValue("totalMiner", _mineCount);
        }

        public void SaveKeys()
        {
            SaveManager.SaveValue("totalMiner" ,_mineCount);
        }
    }
}
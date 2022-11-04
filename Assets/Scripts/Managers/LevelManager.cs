using Data.UnityObject;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Commands;
using Commands.Level;
using Interfaces;

namespace Managers
{
    public class LevelManager : MonoBehaviour,ISaveLoad
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public int Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;

        #endregion

        #region Private Variables

        private LevelLoaderCommand _levelLoader;
        private ClearActiveLevelCommand _levelClearer;
        private int _levelID;

        #endregion

        #endregion

        private void Awake()
        {
            LoadKeys();
            Init();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onLevelInitialize += OnInitializeLevel;
            LevelSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            LevelSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
        }

        private void OnDisable()
        {
            SaveKeys();
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            OnInitializeLevel();
            SetLevelText();
        }
        
        private void Init()
        {
            _levelClearer = new ClearActiveLevelCommand();
            _levelLoader = new LevelLoaderCommand();
        }

        private int GetLevelCount()
        {
            return _levelID % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
        }
        
        private int OnGetLevelID()
        {
            return _levelID;
        }

        private void SetLevelText()
        {
            BaseSignals.Instance.onSetBaseLevelText?.Invoke(_levelID);
        }
        private void OnInitializeLevel()
        {
            int newLevelData = GetLevelCount();
            _levelLoader.InitializeLevel(newLevelData, levelHolder.transform);
        }
        private void OnClearActiveLevel()
        {
            _levelClearer.ClearActiveLevel(levelHolder.transform);
        }

        public void LoadKeys()
        {
            _levelID = SaveManager.LoadValue("LevelID", _levelID);
        }

        public void SaveKeys()
        {
            SaveManager.SaveValue("LevelID",_levelID);
        }
    }
}
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

        [SerializeField] private TextMeshPro moneyTMP, gemTMP;

        #endregion

        #region Private Variables

        private int _score, _idleScore,_idleOldScore;
        [ShowInInspector] private GameObject _playerGO;
        private SetScoreCommand _setScoreCommand;
        private GameObject _parentGO;
        private bool _isActive = false;
        private int _savedScore;

        #endregion

        #endregion

        private void Awake()
        {
            _savedScore = GetActiveLevel();
            Init();
        }
        
        private void Init()
        {
            _setScoreCommand = new SetScoreCommand(ref _score);
        }
        
        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onSetScore += OnUpdateScore;
            ScoreSignals.Instance.onSetLeadPosition += OnSetLead;
            LevelSignals.Instance.onRestartLevel += OnReset;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            ScoreSignals.Instance.onGetIdleScore += OnGetCurrentScore;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onSetScore -= OnUpdateScore; 
            ScoreSignals.Instance.onSetLeadPosition -= OnSetLead;
            LevelSignals.Instance.onRestartLevel -= OnReset;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            ScoreSignals.Instance.onGetIdleScore -= OnGetCurrentScore;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        #region Event Methods

        private void OnSetLead(GameObject gO)
        {
            _parentGO = gO;
        }
        
        private void OnReset()
        {
            _isActive = false;
        }

        private void OnLevelSuccessful()
        {
            _savedScore = GetActiveLevel();
            ScoreSignals.Instance.onGetScore?.Invoke(_idleScore);
        }

        private void OnUpdateScore(int score)
        {
            if (_idleOldScore < 0) return;
            _idleScore = _idleOldScore + score;
            _setScoreCommand.Execute(_idleScore);
            _idleOldScore = _idleScore;
        }
        
        private int GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Collectable") ? ES3.Load<int>("Collectable") : 0;
        }
        
        private int OnGetCurrentScore()
        {
            return _idleScore;
        }

        private void OnNextLevel()
        {
            Transform transform1;
            (transform1 = transform).SetParent(null);
            transform1.localScale = Vector3.one;
        }

        #endregion
        
        #region Methods

        private void SetScoreManagerPosition()
        {
            transform.position = _parentGO.transform.position + new Vector3(0, 2f, 0);
        }

        private void SetScoreManagerRotation()
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z * -1f);
        }

        #endregion
    }
}
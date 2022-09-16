using System.Collections.Generic;
using Controllers;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private List<GameObject> panels;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshPro scoreTMP;
        [SerializeField] private TextMeshProUGUI idleScoreText;

        #endregion

        #region Private Variables
        private UIPanelController _uiPanelController;
        private bool _isReadyForIdleGame = false;
        #endregion

        #endregion

        private void Awake()
        {
            _uiPanelController = new UIPanelController();
        }
        

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetLevelText += OnSetLevelText;
            UISignals.Instance.onSetScoreText += OnSetScoreText;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetLevelText -= OnSetLevelText;
            UISignals.Instance.onSetScoreText -= OnSetScoreText;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Event Methods

        private void OnPlay()
        {
            
        }

        private void OnOpenPanel(UIPanels panelParam)
        {
            _uiPanelController.OpenPanel(panelParam , panels);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            _uiPanelController.ClosePanel(panelParam , panels);
        }
        
        private void OnSetScoreText(int value)
        {
            scoreTMP.text = (value.ToString());
            idleScoreText.text = (value.ToString());
        }

        private void OnSetLevelText(int value)
        {
            levelText.text = "Level " + (value + 1);
        }

        #endregion

        #region Buttons

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }
        
        
        public void NextLevel()
        {
            LevelSignals.Instance.onNextLevel?.Invoke();
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        public void Claim()
        {
            
        }
        
        
        #endregion
    }
}
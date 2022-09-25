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

        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI gemText;

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
            //_uiPanelController.OpenPanel(panelParam , panels);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            //_uiPanelController.ClosePanel(panelParam , panels);
        }
        
        private void OnSetScoreText(int value)
        {
            
        }

        private void OnSetLevelText(int value)
        {
            
        }

        #endregion

        #region Buttons

        public void Settings()
        {
            var isOpen = settingsPanel.activeInHierarchy;
            if (isOpen)
            {
                settingsPanel.SetActive(false);
            }
            else
            {
                settingsPanel.SetActive(true);
            }
            
        }

        #endregion
    }
}
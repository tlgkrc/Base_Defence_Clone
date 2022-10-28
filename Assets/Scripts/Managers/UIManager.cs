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
        [SerializeField] private TextMeshProUGUI diamondText;

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
            UISignals.Instance.onSetMoneyText += OnSetMoneyText;
            UISignals.Instance.onSetDiamondText += OnSetDiamondText;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onSetMoneyText -= OnSetMoneyText;
            UISignals.Instance.onSetDiamondText -= OnSetDiamondText;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

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

        private void OnSetMoneyText(int totalMoney)
        {
            moneyText.text = totalMoney.ToString();
        }

        private void OnSetDiamondText(int totalDiamond)
        {
            diamondText.text = totalDiamond.ToString();
        }

    }
}
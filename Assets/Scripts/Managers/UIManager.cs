using System;
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
        [SerializeField] private GameObject shopPanel;
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
            UISignals.Instance.onOpenShopPanel += OnOpenShopPanel;
            UISignals.Instance.onCloseShopPanel += OnCloseShopPanel;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onSetMoneyText -= OnSetMoneyText;
            UISignals.Instance.onSetDiamondText -= OnSetDiamondText;
            UISignals.Instance.onOpenShopPanel -= OnOpenShopPanel;
            UISignals.Instance.onCloseShopPanel -= OnCloseShopPanel;
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

        private void OnOpenShopPanel()
        {
            shopPanel.SetActive(true);
        }

        private void OnCloseShopPanel()
        {
            CloseShopPanel();
        }

        public void CloseShopPanel()
        {
            shopPanel.SetActive(false);
        }

        public void UpgradePistol()
        {
            UISignals.Instance.onUpgradeWeapon?.Invoke(WeaponTypes.Pistol);
        }
        
        public void UpgradeShotgun()
        {
            UISignals.Instance.onUpgradeWeapon?.Invoke(WeaponTypes.Shotgun);
        }
        
        public void UpgradeSubmachine()
        {
            UISignals.Instance.onUpgradeWeapon?.Invoke(WeaponTypes.SubMachine);
        }
        
        public void UpgradeRifle()
        {
            UISignals.Instance.onUpgradeWeapon?.Invoke(WeaponTypes.Rifle);
        }

        public void SelectPistol()
        {
            UISignals.Instance.onHoldWeapon?.Invoke(WeaponTypes.Pistol);
        }
        
        public void SelectShotgun()
        {
            UISignals.Instance.onHoldWeapon?.Invoke(WeaponTypes.Shotgun);
        }
        
        public void SelectSubmachine()
        {
            UISignals.Instance.onHoldWeapon?.Invoke(WeaponTypes.SubMachine);
        }
        
        public void SelectRifle()
        {
            UISignals.Instance.onHoldWeapon?.Invoke(WeaponTypes.Rifle);
        }

    }
}
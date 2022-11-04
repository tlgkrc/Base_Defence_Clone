using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject.Base;
using Enums;
using Interfaces;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ShopManager : MonoBehaviour ,ISaveLoad
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI pistolText;
        [SerializeField] private TextMeshProUGUI shotgunText;
        [SerializeField] private TextMeshProUGUI subMachineText;
        [SerializeField] private TextMeshProUGUI riffleText;
        [SerializeField] private List<TextMeshProUGUI> buyOrUpgradeText;

        #endregion

        #region Private Variables

        private ShopData _shopData;
        private int _pistolLevel;
        private int _shotgunLevel;
        private int _subMachineLevel;
        private int _rifleLevel;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            LoadKeys();
            SetShopText();
        }

        private ShopData GetShopData()
        {
            return Resources.Load<CD_ShopData>("Data/CD_ShopData").ShopData;
        }

        private void GetReferences()
        {
            _shopData = GetShopData();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onUpgradeWeapon += OnUpgradeWeapon;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onUpgradeWeapon -= OnUpgradeWeapon;
        }

        private void OnDisable()
        {
            SaveKeys();
            UnsubscribeEvents();
        }

        #endregion

        private void OnUpgradeWeapon(WeaponTypes weaponTypes)
        {
            int? currentMoney = ScoreSignals.Instance.onGetMoneyScore?.Invoke();
            if (currentMoney>0)
            {
                _shopData.ShopGoDatas[WeaponTypes.Pistol].Level++;
                if (weaponTypes == WeaponTypes.Pistol)
                {
                    var value = _shopData.ShopGoDatas[WeaponTypes.Pistol].Cost +
                                _shopData.ShopGoDatas[WeaponTypes.Pistol].Level *
                                _shopData.ShopGoDatas[WeaponTypes.Pistol].IncreasingFactor;
                    buyOrUpgradeText[(int)WeaponTypes.Pistol].text = "UPGRADE";
                    pistolText.text = value.ToString();
                    ScoreSignals.Instance.onUpdateMoneyScore?.Invoke(-value);
                }
                else if (weaponTypes == WeaponTypes.Shotgun)
                {
                    var value = _shopData.ShopGoDatas[WeaponTypes.Shotgun].Cost +
                                _shopData.ShopGoDatas[WeaponTypes.Shotgun].Level *
                                _shopData.ShopGoDatas[WeaponTypes.Shotgun].IncreasingFactor;
                    _shopData.ShopGoDatas[WeaponTypes.Shotgun].Level++;
                    buyOrUpgradeText[(int)WeaponTypes.Shotgun].text = "UPGRADE";
                    shotgunText.text = value.ToString();
                    ScoreSignals.Instance.onUpdateMoneyScore?.Invoke(-value);
                }
                else if (weaponTypes == WeaponTypes.SubMachine)
                {
                    var value = _shopData.ShopGoDatas[WeaponTypes.SubMachine].Cost +
                                _shopData.ShopGoDatas[WeaponTypes.SubMachine].Level *
                                _shopData.ShopGoDatas[WeaponTypes.SubMachine].IncreasingFactor;
                    _shopData.ShopGoDatas[WeaponTypes.SubMachine].Level++;
                    buyOrUpgradeText[(int)WeaponTypes.SubMachine].text = "UPGRADE";
                    subMachineText.text = value.ToString();
                    ScoreSignals.Instance.onUpdateMoneyScore?.Invoke(-value);
                }
                else if (weaponTypes == WeaponTypes.Rifle)
                {
                    var value = _shopData.ShopGoDatas[WeaponTypes.Rifle].Cost +
                                _shopData.ShopGoDatas[WeaponTypes.Rifle].Level *
                                _shopData.ShopGoDatas[WeaponTypes.Rifle].IncreasingFactor;
                    _shopData.ShopGoDatas[WeaponTypes.Rifle].Level++;
                    buyOrUpgradeText[(int)WeaponTypes.Rifle].text = "UPGRADE";
                    riffleText.text = value.ToString();
                    ScoreSignals.Instance.onUpdateMoneyScore?.Invoke(-value);
                }
            }
        }

        private void SetShopText()
        {
            foreach (var item in _shopData.ShopGoDatas)
            {
                var value = item.Value.Cost + item.Value.Level * item.Value.IncreasingFactor;
                if (item.Key == WeaponTypes.Pistol)
                {
                    pistolText.text = value.ToString();
                }
                else if (item.Key == WeaponTypes.Shotgun)
                {
                    shotgunText.text = value.ToString();
                }
                else if (item.Key == WeaponTypes.SubMachine)
                {
                    subMachineText.text = value.ToString();
                }
                else if(item.Key == WeaponTypes.Rifle)
                {
                    riffleText.text = value.ToString();
                }
            }
        }

        public void LoadKeys()
        {
            _shopData.ShopGoDatas[WeaponTypes.Pistol] = SaveManager.LoadValue(WeaponTypes.Pistol.ToString(), _shopData.ShopGoDatas[WeaponTypes.Pistol]);
            _shopData.ShopGoDatas[WeaponTypes.Shotgun] = SaveManager.LoadValue(WeaponTypes.Shotgun.ToString(), _shopData.ShopGoDatas[WeaponTypes.Shotgun]);
            _shopData.ShopGoDatas[WeaponTypes.SubMachine] = SaveManager.LoadValue(WeaponTypes.SubMachine.ToString(), _shopData.ShopGoDatas[WeaponTypes.SubMachine]);
            _shopData.ShopGoDatas[WeaponTypes.Rifle] = SaveManager.LoadValue(WeaponTypes.Rifle.ToString(), _shopData.ShopGoDatas[WeaponTypes.Rifle]);
        }

        public void SaveKeys()
        {
            SaveManager.SaveValue(WeaponTypes.Pistol.ToString(), _shopData.ShopGoDatas[WeaponTypes.Pistol]);
            SaveManager.SaveValue(WeaponTypes.Shotgun.ToString(), _shopData.ShopGoDatas[WeaponTypes.Shotgun]);
            SaveManager.SaveValue(WeaponTypes.SubMachine.ToString(), _shopData.ShopGoDatas[WeaponTypes.SubMachine]);
            SaveManager.SaveValue(WeaponTypes.Rifle.ToString(), _shopData.ShopGoDatas[WeaponTypes.Rifle]);
        } 
    }
}
using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject.Base;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ShopManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI pistolText, shotgunText, subMachineText, riffleText;
        [SerializeField] private List<TextMeshProUGUI> buyOrUpgradeText;

        #endregion

        #region Private Variables

        private ShopData _shopData;


        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
        }

        private ShopData GetShopData()
        {
            return Resources.Load<CD_ShopData>("Data/CD_ShopData").ShopData;
        }

        private void GetReferences()
        {
            _shopData = GetShopData();
            pistolText.text = _shopData.ShopGoDatas[WeaponTypes.Pistol].Cost.ToString();
            shotgunText.text = _shopData.ShopGoDatas[WeaponTypes.Shotgun].Cost.ToString();
            subMachineText.text = _shopData.ShopGoDatas[WeaponTypes.SubMachine].Cost.ToString();
            riffleText.text = _shopData.ShopGoDatas[WeaponTypes.Rifle].Cost.ToString();
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
            UnsubscribeEvents();
        }

        #endregion

        private void OnUpgradeWeapon(WeaponTypes weaponTypes)
        {
            _shopData.ShopGoDatas[WeaponTypes.Pistol].Level++;
            if (weaponTypes == WeaponTypes.Pistol)
            {
                buyOrUpgradeText[(int)WeaponTypes.Pistol].text = "UPGRADE";
                pistolText.text = (_shopData.ShopGoDatas[WeaponTypes.Pistol].Cost +
                                  _shopData.ShopGoDatas[WeaponTypes.Pistol].Level *
                                  _shopData.ShopGoDatas[WeaponTypes.Pistol].IncreasingFactor).ToString();
            }
            else if (weaponTypes == WeaponTypes.Shotgun)
            {
                _shopData.ShopGoDatas[WeaponTypes.Shotgun].Level++;
                buyOrUpgradeText[(int)WeaponTypes.Shotgun].text = "UPGRADE";
                shotgunText.text = (_shopData.ShopGoDatas[WeaponTypes.Shotgun].Cost +
                                   _shopData.ShopGoDatas[WeaponTypes.Shotgun].Level *
                                   _shopData.ShopGoDatas[WeaponTypes.Shotgun].IncreasingFactor).ToString();
            }
            else if (weaponTypes == WeaponTypes.SubMachine)
            {
                _shopData.ShopGoDatas[WeaponTypes.SubMachine].Level++;
                buyOrUpgradeText[(int)WeaponTypes.SubMachine].text = "UPGRADE";
                subMachineText.text = (_shopData.ShopGoDatas[WeaponTypes.SubMachine].Cost +
                                    _shopData.ShopGoDatas[WeaponTypes.SubMachine].Level *
                                    _shopData.ShopGoDatas[WeaponTypes.SubMachine].IncreasingFactor).ToString();
            }
            else if (weaponTypes == WeaponTypes.Rifle)
            {
                _shopData.ShopGoDatas[WeaponTypes.Rifle].Level++;
                buyOrUpgradeText[(int)WeaponTypes.Rifle].text = "UPGRADE";
                riffleText.text = (_shopData.ShopGoDatas[WeaponTypes.Rifle].Cost +
                                    _shopData.ShopGoDatas[WeaponTypes.Rifle].Level *
                                    _shopData.ShopGoDatas[WeaponTypes.Rifle].IncreasingFactor).ToString();
            }
        }
    }
}
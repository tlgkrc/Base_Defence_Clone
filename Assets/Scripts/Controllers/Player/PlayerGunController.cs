﻿using System;
using System.Collections.Generic;
using Data.ValueObject;
using Data.ValueObject.Weapon;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerGunController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private List<GameObject> weaponList;

        #endregion

        #region Private Variables

        private WeaponData _weaponData;
        private int _currentWeaponIndex;

        #endregion

        #endregion
        

        public void SetWeaponData(WeaponData weaponData)
        {
            _weaponData = weaponData;
            SetDefaultProperties();
        }

        private void SetDefaultProperties()
        {
            foreach (var value in weaponList)
            {
                value.SetActive(false);
            }
            weaponList[0].SetActive(false);
            _currentWeaponIndex = 0;
        }

        public void TakeHandWeapon(WeaponTypes weaponTypes)
        {
            _currentWeaponIndex = (int)weaponTypes;
        }

        public void SetWeaponVisual(bool isOpen)
        {
            weaponList[_currentWeaponIndex].SetActive(isOpen);
        }

    }
}
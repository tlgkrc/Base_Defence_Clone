using System;
using System.Collections.Generic;
using Data.ValueObject;
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
        

        public void SetGunData(WeaponData weaponData)
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
            weaponList[0].SetActive(true);
            _currentWeaponIndex = 0;
        }

        public void TakeHandWeapon(WeaponTypes weaponTypes)
        {
            weaponList[_currentWeaponIndex].SetActive(false);
            if (weaponTypes == WeaponTypes.Pistol)
            {
                weaponList[0].SetActive(true);
                _currentWeaponIndex = 0;
            }
            else if (weaponTypes == WeaponTypes.Shotgun)
            {
                weaponList[1].SetActive(true);
                _currentWeaponIndex = 1;
            }
            else if (weaponTypes == WeaponTypes.SubMachine)
            {
                weaponList[2].SetActive(true);
                _currentWeaponIndex = 2;
            }
            else if (weaponTypes == WeaponTypes.Rifle)
            {
                weaponList[3].SetActive(true);
                _currentWeaponIndex = 3;
            }
        }

        public void SetWeaponVisual(bool isOpen)
        {
            weaponList[_currentWeaponIndex].SetActive(isOpen);
        }

    }
}
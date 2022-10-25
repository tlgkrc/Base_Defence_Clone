using System;
using Data.ValueObject.Base;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class RoomManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private TextMeshPro roomCostTMP;

        #endregion

        #region Private Variables

        private bool _isPaid;
        private TurretData _turretData;

        #endregion

        #endregion

        private void Awake()
        {
            //_turretData = GetRoomTurretData;
            
        }

        private TurretData GetRoomTurretData()
        {
            return null;
        }
    }
}
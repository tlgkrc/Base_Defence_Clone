using System;
using System.Collections;
using Controllers.AreaController;
using Data.UnityObject;
using Data.ValueObject.Base;
using Enums.Animations;
using Signals;
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
        [SerializeField] private RoomNames roomName;
        [SerializeField] private RoomPhysicsController roomPhysicsController;

        #endregion

        #region Private Variables

        private bool _isPaid;
        private TurretData _turretData;
        private RoomData _roomData;

        #endregion

        #endregion

        private void Awake()
        {
            _roomData = GetRoomData();
            GetReferences();
        }

        private RoomData GetRoomData()
        {
            return Resources.Load<CD_BaseData>("Data/CD_BaseData").BaseData.BaseGoData.RoomsData[roomName];
        }

        private void GetReferences()
        {
            roomCostTMP.text = _roomData.Cost.ToString();
        }

        #region Subscription Events

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
        }

        private void UnsubscribeEvents()
        {
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public void BuyRoom()
        {
            StartCoroutine(Buy());
        }

        public void StopBuying()
        {
            StopCoroutine(Buy());
        }

        IEnumerator Buy()
        {
            int moneyToPay = _roomData.Cost - _roomData.PaidAmount;
            if(moneyToPay <= 0)
            {
                OpenRoom();
                yield break;
            }
            _roomData.PaidAmount++;
            roomPhysicsController.SetRadialVisual(moneyToPay ,_roomData.Cost);
            yield return new WaitForSeconds(_roomData.BuyDelay);
        }

        private void OpenRoom()
        {
            
        }
    }
}
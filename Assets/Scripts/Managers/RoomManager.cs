using System.Collections;
using Controllers.AreaController;
using Data.UnityObject;
using Data.ValueObject.Base;
using Enums.Animations;
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
        [SerializeField] private GameObject openedArea;
        [SerializeField] private GameObject closedArea;

        #endregion

        #region Private Variables

        private bool _isPaid;
        private TurretData _turretData;
        private RoomData _roomData;
        private int _moneyToPay;
        private bool _isOnArea;

        #endregion

        #endregion

        private void Awake()
        {
            _roomData = GetRoomData();
            GetReferences();
            SetRoomMoney(_roomData.Cost);
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
            _isOnArea = true;
            StartCoroutine(Buy());
        }

        public void StopBuying()
        {
            _isOnArea = false;
            StopCoroutine(Buy());
        }

        private IEnumerator Buy()
        {
            while (_isOnArea)
            {
                _moneyToPay = _roomData.Cost - _roomData.PaidAmount;
                if(_moneyToPay == 0)
                {
                    OpenRoom();
                    yield break;
                }
                _roomData.PaidAmount++;
                SetRoomMoney(_moneyToPay);
                yield return new WaitForSeconds(_roomData.BuyDelay);
            }
            
        }

        private void OpenRoom()
        {
            closedArea.SetActive(false);
            openedArea.SetActive(true);
        }

        private void SetRoomMoney(int money)
        {
            roomCostTMP.text = "<sprite index=0>" + money;
        }
    }
}
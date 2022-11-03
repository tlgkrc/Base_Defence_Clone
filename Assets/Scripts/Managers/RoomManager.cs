using System.Collections;
using Controllers.AreaController;
using Data.UnityObject;
using Data.ValueObject.Base;
using Enums.Animations;
using Interfaces;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class RoomManager : MonoBehaviour ,ISaveLoad
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
            if (_roomData.PaidAmount >= _roomData.Cost)
            {
                OpenRoom();
            }
            else 
            {
                SetRoomMoney(_roomData.Cost-_roomData.PaidAmount);
            }
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
                int? currentMoney = ScoreSignals.Instance.onGetMoneyScore?.Invoke();
                
                if (currentMoney >0)
                {
                    _moneyToPay = _roomData.Cost - _roomData.PaidAmount;
                    if(_moneyToPay == 0)
                    {
                        OpenRoom();
                        yield break;
                    }
                    _roomData.PaidAmount++;
                    ScoreSignals.Instance.onUpdateMoneyScore?.Invoke(-1);
                    SetRoomMoney(_moneyToPay);
                    roomPhysicsController.SetRadialVisual(_roomData.PaidAmount, _roomData.Cost);
                    yield return new WaitForSeconds(_roomData.BuyDelay);
                }
                else
                {
                    yield break;
                }
                
            }
            
        }

        private void OpenRoom()
        {
            closedArea.SetActive(false);
            openedArea.SetActive(true);
        }

        private void SetRoomMoney(int money)
        {
            roomCostTMP.text = money.ToString();
        }

        public void LoadKeys()
        {
            _roomData.PaidAmount = SaveManager.LoadValue("paidAmount" + GetInstanceID().ToString(),_roomData.PaidAmount);
        }

        public void SaveKeys()
        {
            SaveManager.SaveValue("paidAmount" + GetInstanceID().ToString(),_roomData.PaidAmount);
        }
    }
}
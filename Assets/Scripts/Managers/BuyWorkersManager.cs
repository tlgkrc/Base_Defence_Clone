using System.Collections;
using Data.UnityObject;
using Data.ValueObject.Base;
using Interfaces;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class BuyWorkersManager : MonoBehaviour,ISaveLoad
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private TextMeshPro ammoWorkerCostTMP;
        [SerializeField] private TextMeshPro moneyWorkerCostTMP;
        [SerializeField] private GameObject ammoWorker;
        [SerializeField] private GameObject moneyWorker;
        [SerializeField] private GameObject hireAmmoWorkerTMP;
        [SerializeField] private GameObject hireMoneyWorkerTMP;
        [SerializeField] private GameObject upgradeAWorkerTMP;
        [SerializeField] private GameObject upgradeMWorkerTMP;

        #endregion

        #region Private Variables

        private bool _playerInAmmoArea;
        private bool _playerInMoneyArea;
        private BuyWorkerData _buyWorkerData;
        private int _moneyToPayForAmmoWorker;
        private int _moneyToPayForMoneyWorker;
        private bool _isHiredMWorker;
        private bool _isHiredAWorker;

        #endregion

        #endregion

        private void Awake()
        {
            _buyWorkerData = GetCostData();
        }

        #region Event Subscription

        private void OnEnable()
        {
            LoadKeys();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }
        
        private void UnSubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            SaveKeys();
            UnSubscribeEvents();
        }

        #endregion

        private void Start()
        {
            if (_isHiredAWorker)
            {
                HireAmmoWorker();
            }
            else
            {
                SetAmmoWorkerCostText(_buyWorkerData.CostOfAmmoWorker);
            }

            if (_isHiredMWorker)
            {
                HireMoneyWorker();
            }
            else
            {
                SetMoneyWorkerCostText(_buyWorkerData.CostOfMoneyWorker);
            }
        }

        private BuyWorkerData GetCostData()
        {
            return Resources.Load<CD_BaseData>("Data/CD_BaseData").BaseData.BaseGoData.BuyWorkerData;
        }

        public void BuyAmmoWorker()
        {
            SetAmmoAreaState(true);
            StartCoroutine(BuyAWorker());
        }

        public void StopAmmoWorkerBuying()
        {
            SetAmmoAreaState(false);
            StopCoroutine(BuyAWorker());
        }

        public void BuyMoneyWorker()
        {
            SetMoneyAreaState(true);
            StartCoroutine(BuyMWorker());
        }

        public void StopMoneyWorkerBuying()
        {
            SetMoneyAreaState(false);
            StopCoroutine(BuyMWorker());
        }

        private void SetAmmoAreaState(bool isInArea)
        {
            _playerInAmmoArea = isInArea;
        }

        private void SetMoneyAreaState(bool isInArea)
        {
            _playerInMoneyArea = isInArea;
        }

        private void SetAmmoWorkerCostText(int cost)
        {
            ammoWorkerCostTMP.text = cost.ToString();
        }

        private void SetMoneyWorkerCostText(int cost)
        {
            moneyWorkerCostTMP.text = cost.ToString();
        }

        IEnumerator BuyMWorker()
        {
            while (_playerInMoneyArea && !_isHiredMWorker)
            {
                _moneyToPayForMoneyWorker = _buyWorkerData.CostOfMoneyWorker - _buyWorkerData.PaidAmountForMoneyWorker;
                SetMoneyWorkerCostText(_moneyToPayForMoneyWorker);
                if (_moneyToPayForMoneyWorker==0)
                {
                    HireMoneyWorker();
                    yield break;
                }
                _buyWorkerData.PaidAmountForMoneyWorker++;
                ScoreSignals.Instance.onUpdateMoneyScore?.Invoke(-1);
                yield return new WaitForSeconds(_buyWorkerData.DelayTime);
            }
        }

        private void HireMoneyWorker()
        {
            moneyWorker.SetActive(true);
            hireMoneyWorkerTMP.SetActive(false);
            upgradeMWorkerTMP.SetActive(true);
            _isHiredMWorker = true;
            moneyWorkerCostTMP.transform.parent.gameObject.SetActive(false);
        }

        IEnumerator BuyAWorker()
        {
            while (_playerInAmmoArea && !_isHiredAWorker)
            {
                int? currentMoney = ScoreSignals.Instance.onGetMoneyScore?.Invoke();
                if (currentMoney>0)
                {
                    _moneyToPayForAmmoWorker = _buyWorkerData.CostOfAmmoWorker - _buyWorkerData.PaidAmountForAmmoWorker;
                    SetAmmoWorkerCostText(_moneyToPayForAmmoWorker);
                    if (_moneyToPayForAmmoWorker==0)
                    {
                        HireAmmoWorker();
                        yield break;
                    }
                    _buyWorkerData.PaidAmountForAmmoWorker++;
                    ScoreSignals.Instance.onUpdateMoneyScore?.Invoke(-1);
                    yield return new WaitForSeconds(_buyWorkerData.DelayTime);
                }
                else
                {
                    yield break;
                }
            }
        }

        private void HireAmmoWorker()
        {
            ammoWorker.SetActive(true);
            hireAmmoWorkerTMP.SetActive(false);
            upgradeAWorkerTMP.SetActive(true);
            _isHiredAWorker = true;
            ammoWorkerCostTMP.transform.parent.gameObject.SetActive(false);
        }

        public void LoadKeys()
        {
            _isHiredAWorker = SaveManager.LoadValue("_isHiredAWorker",_isHiredAWorker);
            _isHiredMWorker = SaveManager.LoadValue("_isHiredMWorker",_isHiredMWorker);
            _moneyToPayForAmmoWorker =
                SaveManager.LoadValue("_paidAmountForAmmoWorker", _buyWorkerData.PaidAmountForAmmoWorker);
            _moneyToPayForMoneyWorker =
                SaveManager.LoadValue("_paidAmountForMoneyWorker", _buyWorkerData.PaidAmountForMoneyWorker);
        }

        public void SaveKeys()
        {
            SaveManager.SaveValue("_isHiredAWorker",_isHiredAWorker);
            SaveManager.SaveValue("_isHiredMWorker",_isHiredMWorker);
            SaveManager.SaveValue("_paidAmountForAmmoWorker",_buyWorkerData.PaidAmountForAmmoWorker);
            SaveManager.SaveValue("_paidAmountForMoneyWorker",_buyWorkerData.PaidAmountForMoneyWorker);
        }
    }
}
using System.Collections;
using Controllers.AreaController;
using Data.UnityObject;
using Data.ValueObject.Base;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class BuyWorkersManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private BuyAmmoWorkerPhysicController buyAmmoWorkerPhysicController;
        [SerializeField] private BuyMoneyWorkerPhysicController buyMoneyWorkerPhysicController;
        [SerializeField] private TextMeshPro ammoWorkerCostTMP;
        [SerializeField] private TextMeshPro moneyWorkerCostTMP;
        [SerializeField] private GameObject ammoWorker;
        [SerializeField] private GameObject moneyWorker;
        [SerializeField] private GameObject hireAmmoWorkerTMP,hireMoneyWorkerTMP;
        [SerializeField] private GameObject upgradeAWorkerTMP, upgradeMWorkerTMP;

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
            SetAmmoWorkerCostText(_buyWorkerData.CostOfAmmoWorker);
            SetMoneyWorkerCostText(_buyWorkerData.CostOfMoneyWorker);
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
                _buyWorkerData.PaidAmountForMoneyWorker++;
                yield return new WaitForSeconds(_buyWorkerData.DelayTime);
                if (_moneyToPayForMoneyWorker==0)
                {
                    HireMoneyWorker();
                    yield break;
                }
            }
            
        }

        private void HireMoneyWorker()
        {
            moneyWorker.SetActive(true);
            hireMoneyWorkerTMP.SetActive(false);
            upgradeMWorkerTMP.SetActive(true);
            _isHiredMWorker = true;
        }

        IEnumerator BuyAWorker()
        {
            while (_playerInAmmoArea && !_isHiredAWorker)
            {
                _moneyToPayForAmmoWorker = _buyWorkerData.CostOfAmmoWorker - _buyWorkerData.PaidAmountForAmmoWorker;
                SetAmmoWorkerCostText(_moneyToPayForAmmoWorker);
                _buyWorkerData.PaidAmountForAmmoWorker++;
                yield return new WaitForSeconds(_buyWorkerData.DelayTime);
                if (_moneyToPayForAmmoWorker==0)
                {
                    HireAmmoWorker();
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
        }
    }
}
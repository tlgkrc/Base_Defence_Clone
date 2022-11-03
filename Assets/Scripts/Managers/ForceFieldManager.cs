using System;
using System.Collections;
using Data.UnityObject;
using Data.ValueObject.Base;
using DG.Tweening;
using Interfaces;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ForceFieldManager : MonoBehaviour,ISaveLoad
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables
        
        [SerializeField] private TextMeshPro forceFieldTMP;
        [SerializeField] private GameObject forceField;

        #endregion

        #region Private Variables
        
        private int _totalCost = 50;
        private int _paidCost = 0;
        private bool _playerInArea = false;
        private int _diamondToPayForField;

        #endregion

        #endregion

        private void Awake()
        {
            LoadKeys();
        }

        private void SetDiamondToText()
        {
            forceFieldTMP.text = (_totalCost - _paidCost).ToString();
        }

        private void Start()
        {
            SetDiamondToText();
            if (_paidCost>=_totalCost)
            {
                BuyForceField();
            }
        }

        #region Event Subscription

        private void OnEnable()
        {
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
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("inZone");
                _playerInArea = true;
                StartCoroutine(BuyForceFieldRoutine());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerInArea = false;
                StopCoroutine(BuyForceFieldRoutine());
            }
        }

        private void BuyForceField()
        {
            forceField.SetActive(false);
        }


        IEnumerator BuyForceFieldRoutine()
        {
            while (_playerInArea)
            {
                int? diamond = ScoreSignals.Instance.onGetDiamondScore?.Invoke();
                _diamondToPayForField = _totalCost - _paidCost;
                SetDiamondToText();
                if (_diamondToPayForField==0)
                {
                    forceField.transform.DOScale(Vector3.zero, .4f).SetEase(Ease.InCirc).OnComplete(() => BuyForceField());
                    yield break;
                }

                _paidCost++;
                ScoreSignals.Instance.onUpdateDiamonScore?.Invoke(-1);
                yield return new WaitForSeconds(.1f);
            }
        }


        public void LoadKeys()
        {
            _paidCost = SaveManager.LoadValue("forceFieldPaidAmount_" + GetInstanceID().ToString(),_paidCost);
        }

        public void SaveKeys()
        {
            SaveManager.SaveValue("forceFieldPaidAmount_" + GetInstanceID().ToString(),_paidCost);
        }
    }
}
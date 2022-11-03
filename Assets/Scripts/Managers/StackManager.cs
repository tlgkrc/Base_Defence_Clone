using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Helpers;
using Interfaces;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class StackManager : MonoBehaviour,IStack
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject stackGameObject;
        [SerializeField] private StackTypes stackType;

        #endregion

        #region Private Variables

        [ShowInInspector] private List<GameObject> _stackList = new List<GameObject>();
        [ShowInInspector] private StackGOData _stackGoData;
        private Vector3 _stackStartPosition;
        private Vector3 _stackPos;
        private GridSystem _gridSystem;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _stackGoData = GetStackData();
            _gridSystem = new GridSystem(transform, _stackGoData.MaxCount, _stackGoData.Offset, _stackGoData.Grid_1,
                _stackGoData.Grid_2, _stackGoData.BaseAxis);
        }

        private StackGOData GetStackData()
        {
            return Resources.Load<CD_StackData>("Data/CD_StackData").Datas.StackDatas[stackType];
        }

        #region Subscription Events

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onAddStack += OnAddStack;
            StackSignals.Instance.onClearStaticStack += OnClearStaticStack;
            StackSignals.Instance.onClearDynamicStack += OnClearDynamicStack;
            StackSignals.Instance.onTransferBetweenStacks += OnTransferBetweenStacks;
            StackSignals.Instance.onRemoveLastElement += OnRemoveLastElement;
            StackSignals.Instance.onAddToPlayer += OnAddAmmoBoxToPlayer;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddStack -= OnAddStack;
            StackSignals.Instance.onClearStaticStack -= OnClearStaticStack;
            StackSignals.Instance.onClearDynamicStack -= OnClearDynamicStack;
            StackSignals.Instance.onTransferBetweenStacks -= OnTransferBetweenStacks;
            StackSignals.Instance.onRemoveLastElement -= OnRemoveLastElement;
            StackSignals.Instance.onAddToPlayer -= OnAddAmmoBoxToPlayer;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnAddStack(int managerId,GameObject gameObject)
        {
            if (transform.parent.GetInstanceID() == managerId )
            {
                Add(gameObject);
            }
        }

        private void OnClearStaticStack(Transform playerTransform)
        {
            ClearStaticStack(playerTransform);
        }

        private void OnClearDynamicStack(int managerId)
        {
            if (GetInstanceID() == managerId)
            {
                 ClearDynamicStack();
            }
        }

        private void OnTransferBetweenStacks(int managerId,StackManager from,StackManager to)
        {
            if (managerId == to.GetInstanceID())
            {
                 TransferBetweenStacks(from ,to);
            }
           
        }

        private void OnRemoveLastElement(int id,string nameOfGameObject)
        {
            if (transform.GetInstanceID() == id)
            {
                Remove(nameOfGameObject);
            }
        }

        public void Add(GameObject gO)
        {
            _stackList.Add(gO);
            gO.transform.parent = transform;
            if (_stackGoData.IsDynamic)
            {
                if (stackType == StackTypes.Money)
                {
                    gO.GetComponent<Rigidbody>().isKinematic = true;
                    gO.GetComponent<Rigidbody>().useGravity = false;
                    gO.transform.GetChild(1).tag = "Untagged";
                }
                DynamicStackAddingAnimation(gO,gO.transform.localPosition,_gridSystem.NextPoint(_stackList.Count));
            }
            else
            {
                gO.transform.localPosition = _gridSystem.NextPoint(_stackList.Count);
            }
        }

        public void Remove(string nameOfGameObject)
        {
            var lastGo = _stackList[^1];
            _stackList.Remove(lastGo);
            _stackList.TrimExcess();
            PoolSignals.Instance.onReleasePoolObject?.Invoke(nameOfGameObject,lastGo); 
        }

        public void ClearStaticStack(Transform pTransform)
        {
            ScoreSignals.Instance.onUpdateDiamonScore?.Invoke(_stackList.Count);
            foreach (var gO in _stackList) 
            {
                ClearStackAnimation(gO, pTransform);
            }
            
            _stackList.Clear();
        }

        private void ClearStackAnimation(GameObject gO,Transform playerTransform)
        {
            var position = transform.position;
            var newVec = new Vector3(position.x + Random.Range(-1, 1),
                position.y + Random.Range(2, 5), position.z + Random.Range(-1, 1));
            gO.transform.DOMove(newVec, 1f).SetEase(Ease.OutBack).OnComplete(() =>
                gO.transform.DOMove(playerTransform.position + new Vector3(0,1.5f,0), .2f));
            gO.transform.DOShakeScale(1.2f,2).SetEase(Ease.OutBounce).OnComplete(()=> PoolSignals.Instance.onReleasePoolObject(stackGameObject.name,gO));
        }
        
        private void ClearDynamicStack()
        {
            ScoreSignals.Instance.onUpdateMoneyScore?.Invoke(10*_stackList.Count);
            for (int i = _stackList.Count-1; i >= 0; i--)
            {
                var gO = _stackList[i];
                var firstVec3 = new Vector3(Random.Range(-2,2), Random.Range(1, 3), Random.Range(-2, 2)) +transform.position;
                var secondVec3 = new Vector3(Random.Range(-2, 2), Random.Range(1, 3), Random.Range(-2, 2))+transform.position;
                var thirdVec3 = Random.Range(-1, 1) * transform.position;

                gO.transform.DOPath(new Vector3[3] { firstVec3, secondVec3, thirdVec3 }, 3f).
                    OnComplete(() =>PoolSignals.Instance.onReleasePoolObject(stackGameObject.name,gO));
            }
            _stackList.Clear();
            _stackList.TrimExcess();
        }

        private void DynamicStackAddingAnimation(GameObject stackElement,Vector3 startPos ,Vector3 endPos)
        {
            var randomPos = new Vector3(Random.Range(0, 3f) + endPos.x,
                Random.Range(0, 3f) + endPos.y, Random.Range(0, 3f) + endPos.z);
            var randomRotate = new Vector3(Random.Range(30, 300), Random.Range(30, 300), Random.Range(30, 300));
            stackElement.transform.DOLocalMove(randomPos, .2f).OnComplete(() => 
                stackElement.transform.DOLocalMove(endPos,.2f));
            stackElement.transform.DOLocalRotate(randomRotate, .3f).OnComplete(() => 
                stackElement.transform.DOLocalRotate(Vector3.zero,.1f));
        }

        private void TransferBetweenStacks(StackManager from,StackManager to)
        {
            for (var i = from._stackList.Count-1 ; i >= 0; i--)
            {
                var gO = from._stackList[i];
                gO.transform.SetParent(to.transform);
                to._stackList.Add(gO);
                gO.transform.DOLocalMove(to._gridSystem.NextPoint(to._stackList.Count), .5f);
                gO.transform.DOLocalRotate(Vector3.zero, .5f).SetEase(Ease.InBack);
            }
            from._stackList.Clear();
        }

        private void OnAddAmmoBoxToPlayer(int id)
        {
            if (id == transform.parent.GetInstanceID() && stackType == StackTypes.AmmoBox)
            {
                var maxStackCount = StackSignals.Instance.onGetMaxPlayerStackCount?.Invoke();
                for (int i = 0; i < maxStackCount; i++)
                {
                    var bulletBox =
                        PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.BulletBox.ToString(), transform);
                    if (bulletBox == null) continue;
                    _stackList.Add(bulletBox);
                    bulletBox.transform.parent = transform;
                    DynamicStackAddingAnimation(bulletBox, bulletBox.transform.localPosition,
                        _gridSystem.NextPoint(_stackList.Count));
                }
            }
        }
    }
}
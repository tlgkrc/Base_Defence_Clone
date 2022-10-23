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
        [SerializeField] private Transform stackMesh;
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
        //max count override secenegi olcak override edilecek ise seviye ustune eklenicek

        private void Awake()
        {
            GetReferences();
        }

        private void Start()
        {
            _gridSystem = new GridSystem(transform, _stackGoData.MaxCount, _stackGoData.Offset, _stackGoData.Grid_1,
                _stackGoData.Grid_2, _stackGoData.BaseAxis);
        }

        private void GetReferences()
        {
            _stackGoData = GetStackData();
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
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddStack -= OnAddStack;
            StackSignals.Instance.onClearStaticStack -= OnClearStaticStack;
            StackSignals.Instance.onClearDynamicStack -= OnClearDynamicStack;
            StackSignals.Instance.onTransferBetweenStacks -= OnTransferBetweenStacks;
            StackSignals.Instance.onRemoveLastElement -= OnRemoveLastElement;
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
            Debug.Log(managerId +"---" + transform.parent.GetInstanceID() );
            if (transform.parent.GetInstanceID() == managerId)
            {
                 ClearDynamicStack();
            }
        }

        private void OnTransferBetweenStacks(int managerId,StackManager from,StackManager to)
        {
            if (managerId == transform.parent.GetInstanceID())
            {
                 TransferBetweenStacks(from ,to);
            }
           
        }

        private void OnRemoveLastElement(int id,string nameOfGameObject)
        {
            if (transform.GetInstanceID() == id)
            {
                PoolSignals.Instance.onReleasePoolObject?.Invoke(nameOfGameObject,_stackList[^1].gameObject ); 
                _stackList.Remove(_stackList[^1].gameObject);
                _stackList.TrimExcess();
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

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void ClearStaticStack(Transform pTransform)
        {
            foreach (var gO in _stackList) 
            {
                    ClearStackAnimation(gO, pTransform);
            } 
            _stackList.Clear();
        }

        private void ClearStackAnimation(GameObject gO,Transform playerTransform)
        {
            var position = transform.position;
            var newVec = new Vector3(position.x + Random.Range(2, 4),
                position.y + Random.Range(2, 4), position.z + Random.Range(2, 4));
            gO.transform.DOMove(newVec, 1f).SetEase(Ease.InOutBack).OnComplete(() =>
                gO.transform.DOMove(playerTransform.position + new Vector3(0,2,0), 1f));
            gO.transform.DOScale(Vector3.zero, 2f).SetEase(Ease.InElastic).OnComplete(()=> PoolSignals.Instance.onReleasePoolObject(stackGameObject.name,gO));
        }
        
        private void ClearDynamicStack()
        {
           
            for (int i = _stackList.Count-1; i >= 0; i--)
            {
                var i1 = i;
                var increaseVec3 = new Vector3(Random.Range(-2, 2), Random.Range(1, 3), Random.Range(-2, 2));
                if (i >=5)
                {
                    _stackList[i].transform.DOLocalMove(_stackList[^1].transform.localPosition + increaseVec3, .5f)
                        .SetEase(Ease.InSine).OnComplete(() =>
                            _stackList[i1].transform.DOLocalMove(Vector3.zero, .5f).SetEase(Ease.InOutSine)).
                        OnComplete(() => PoolSignals.Instance.onReleasePoolObject(stackGameObject.name,_stackList[i1]));
                }
                else
                {
                    _stackList[i].transform.DOLocalMove(_stackList[^1].transform.localPosition + increaseVec3, .2f)
                        .SetEase(Ease.InSine).OnComplete(() =>
                            _stackList[i1].transform.DOLocalMove(Vector3.zero, .2f).SetEase(Ease.InOutSine).
                                OnComplete(() => PoolSignals.Instance.onReleasePoolObject(stackGameObject.name,_stackList[i1])));
                }
            }
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
                from._stackList[i].transform.SetParent(to.transform);
                to._stackList.Add(_stackList[i]);
                _stackList[i].transform.DOLocalMove(to._gridSystem.NextPoint(to._stackList.Count), .5f);
                _stackList[i].transform.DOLocalRotate(Vector3.zero, .5f).SetEase(Ease.InBack);
                 
            }
            from._stackList.Clear();
        }
    }
}
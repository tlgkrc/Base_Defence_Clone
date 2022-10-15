using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
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

        #endregion

        #region Private Variables

        [ShowInInspector]private List<GameObject> _stackList = new List<GameObject>();
        [ShowInInspector] private StackGOData _stackGoData;
        private Vector3 _stackStartPosition;
        private Vector3 _stackPos;
        private Vector3 _localPos,_localScale;
        private GridSystem _gridSystem;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            CalculateStaticStackStartPos();
            _gridSystem = new GridSystem(transform, _stackGoData.maxCount, _stackGoData.Offset, _stackGoData.Grid_1,
                _stackGoData.Grid_2, _stackGoData.BaseAxis);
        }

        private void GetReferences()
        {
            _stackGoData = GetStackData();
            _localScale = stackMesh.localScale;
            _localPos = transform.position;
        }

        private StackGOData GetStackData()
        {
            return Resources.Load<CD_StackData>("Data/CD_StackData").Datas.StackDatas[stackGameObject];
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
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddStack -= OnAddStack;
            StackSignals.Instance.onClearStaticStack -= OnClearStaticStack;
            StackSignals.Instance.onClearDynamicStack -= OnClearDynamicStack;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnAddStack(int managerId)
        {
            if (transform.parent.GetInstanceID() == managerId )
            {
                Add();
            }
        }

        private void OnClearStaticStack(Transform playerTransform)
        {
            ClearStaticStack(playerTransform);
        }

        private void OnClearDynamicStack(int managerId)
        {
            if (transform.parent.GetInstanceID() == managerId)
            {
                 ClearDynamicStack();
            }
           
        }

        public void Add()
        {
            var go =PoolSignals.Instance.onGetPoolObject(stackGameObject.name, this.transform);
            if (_stackGoData.IsDynamic)
            {

                _stackList.Add(go);
                go.GetComponent<Rigidbody>().isKinematic = true;
                go.GetComponent<Rigidbody>().useGravity = false;
                go.transform.GetChild(1).tag = "Untagged";
                go.transform.parent = transform;
                //go.transform.localPosition = _gridSystem.NextPoint(_stackList.Count);
                DynamicStackAddingAnimation(go,go.transform.localPosition,_gridSystem.NextPoint(_stackList.Count));
                //go.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
            }
            else// Adding to plane base stack
            {
                _stackPos.x += (_stackList.Count % (_stackGoData.Grid_1*_stackGoData.Grid_2)) % _stackGoData.Grid_1 * (_localScale.x * 10) / _stackGoData.Grid_1;
                _stackPos.z += (_stackList.Count % (_stackGoData.Grid_1 *_stackGoData.Grid_2)) / _stackGoData.Grid_2 * (_localScale.z * 10) / _stackGoData.Grid_2;
                _stackPos.y += _stackList.Count / (_stackGoData.Grid_1 * _stackGoData.Grid_2) * _stackGoData.Offset.y;

                _stackList.Add(go);
                go.transform.localPosition = _stackPos;
                
            }
            _stackPos = _stackStartPosition;
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
            //StackCount will be send to scoremanager
        }

        private void CalculateStaticStackStartPos()
        {
            _stackStartPosition.x = _localPos.x -
                                    (_localScale.x * 10) / _stackGoData.Grid_1;
            _stackStartPosition.z = _localPos.z -
                               (_localScale.z * 10) / _stackGoData.Grid_2;
            _stackStartPosition.y = _stackGoData.StartHeight;
            _stackPos = _stackStartPosition;
        }

        //here add async for player last pos
        private void ClearStackAnimation(GameObject gO,Transform playerTransform)
        {
            var position = transform.position;
            var newVec = new Vector3(position.x + Random.Range(2, 4),
                position.y + Random.Range(2, 4), position.z + Random.Range(2, 4));
            gO.transform.DOMove(newVec, 1f).SetEase(Ease.InOutBack).OnComplete(() =>
                gO.transform.DOMove(playerTransform.position + new Vector3(0,2,0), 1f));
            gO.transform.DOScale(Vector3.zero, 2f).SetEase(Ease.InElastic).OnComplete(()=> PoolSignals.Instance.onReleasePoolObject(stackGameObject.name,gO));
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

        private void ClearDynamicStack()
        {
           
            for (int i = _stackList.Count-1; i >= 0; i--)
            {
                var increaseVec3 = new Vector3(Random.Range(-2, 2), Random.Range(1, 3), Random.Range(-2, 2));
                if (i >=5)
                {
                    var i1 = i;
                    _stackList[i].transform.DOLocalMove(_stackList[^1].transform.localPosition + increaseVec3, .5f)
                        .SetEase(Ease.InSine).OnComplete(() =>
                            _stackList[i1].transform.DOLocalMove(Vector3.zero, .5f).SetEase(Ease.InOutSine)).
                        OnComplete(() => PoolSignals.Instance.onReleasePoolObject(stackGameObject.name,_stackList[i1]));
                }
                else
                {
                    var i1 = i;
                    _stackList[i].transform.DOLocalMove(_stackList[^1].transform.localPosition + increaseVec3, .2f)
                        .SetEase(Ease.InSine).OnComplete(() =>
                            _stackList[i1].transform.DOLocalMove(Vector3.zero, .2f).SetEase(Ease.InOutSine).
                                OnComplete(() => PoolSignals.Instance.onReleasePoolObject(stackGameObject.name,_stackList[i1])));
                }
            }
            _stackList.TrimExcess();
        }
        
    }
}
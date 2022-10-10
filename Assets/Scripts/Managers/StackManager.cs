using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
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

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            CalculateStaticStackStartPos();
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
            StackSignals.Instance.onClearStack += OnClearStack;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddStack -= OnAddStack;
            StackSignals.Instance.onClearStack += OnClearStack;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnAddStack()
        {
            Add();
        }

        private void OnClearStack(Transform playerTransform)
        {
            Clear(playerTransform);
        }

        public void Add()
        {
            var go =PoolSignals.Instance.onGetPoolObject(stackGameObject.name, this.transform);
            if (_stackGoData.IsDynamic)
            {
                //stack elements will be located with data.

                _stackList.Add(go);
                go.transform.parent = transform;
                go.transform.localPosition = _stackPos;
            }
            else// Adding to plane base stack
            {
                _stackPos.x += (_stackList.Count % (_stackGoData.GridX*_stackGoData.GridZ)) % _stackGoData.GridX * (_localScale.x * 10) / _stackGoData.GridX;
                _stackPos.z += (_stackList.Count % (_stackGoData.GridX *_stackGoData.GridZ)) / _stackGoData.GridZ * (_localScale.z * 10) / _stackGoData.GridZ;
                _stackPos.y += _stackList.Count / (_stackGoData.GridX * _stackGoData.GridZ) * _stackGoData.LevelOffset;

                _stackList.Add(go);
                go.transform.localPosition = _stackPos;
                
            }
            _stackPos = _stackStartPosition;
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void Clear(Transform pTransform)
        {
            if (!_stackGoData.IsDynamic)
            {
                foreach (var gO in _stackList)
                {
                    ClearAnim(gO, pTransform);
                }
            }
            _stackList.Clear();
            //StackCount will be send to scoremanager
        }

        private void CalculateStaticStackStartPos()
        {
            _stackStartPosition.x = _localPos.x -
                                    (_localScale.x * 10) / _stackGoData.GridX;
            _stackStartPosition.z = _localPos.z -
                               (_localScale.z * 10) / _stackGoData.GridZ;
            _stackStartPosition.y = _stackGoData.StartHeight;
            _stackPos = _stackStartPosition;
        }

        private void ClearAnim(GameObject gO,Transform playerTransform)
        {
            var position = transform.position;
            var newVec = new Vector3(position.x + Random.Range(2, 4),
                position.y + Random.Range(2, 4), position.z + Random.Range(2, 4));
            gO.transform.DOMove(newVec, 1f).SetEase(Ease.InOutBack).OnComplete(() =>
                gO.transform.DOMove(playerTransform.position + new Vector3(0,2,0), 1f));
            gO.transform.DOScale(Vector3.zero, 2f).SetEase(Ease.InElastic).OnComplete(()=> PoolSignals.Instance.onReleasePoolObject(stackGameObject.name,gO));
        }
        
    }
}
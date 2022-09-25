using System;
using System.Collections.Generic;
using System.Linq;
using Data.UnityObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Unity.Mathematics;
using UnityEngine;

namespace Pool
{
    
    public class PoolCreator : MonoBehaviour
    {
        #region Self Variables
    
        #region Private Variables
    
        private Transform _objTransformCache;
        [ShowInInspector]private CD_Pool _poolData;
        private PoolTypes _listCache;
        private Dictionary<PoolTypes,GameObject> _poolGroup = new Dictionary<PoolTypes, GameObject>();
    
        #endregion
    
        #endregion
    
        private void Awake()
        {
            _poolData = Resources.Load<CD_Pool>("Data/CD_Pool");
            CreatGameObjectGroup();
            InitPool();
        }

        private void CreatGameObjectGroup()
        {
            foreach (var value in _poolData.PoolTypeDatas)
            {
                var gameObjectCache = new GameObject
                {
                    name = value.Key.ToString(),
                    transform =
                    {
                        parent = transform
                    }
                };
                _poolGroup.Add(value.Key,gameObjectCache);
            }
        }
        
        #region Event Subscription
    
        private void OnEnable()
        {
            SubscribeEvents();
        }
    
        private void SubscribeEvents()
        {
            PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
            PoolSignals.Instance.onReleasePoolObject += OnReleasePoolObject;
        }
    
        private void UnsubscribeEvents()
        {
            PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
            PoolSignals.Instance.onReleasePoolObject -= OnReleasePoolObject;
        }
    
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    
        #endregion
    
        private GameObject OnGetPoolObject(string poolType,Transform objTransform)
        {
            _listCache = (PoolTypes)Enum.Parse(typeof(PoolTypes), poolType);
            _objTransformCache = objTransform;
            var obj = PoolManager.Instance.GetObject<GameObject>(poolType.ToString());
            return obj;
        }
    
        private void OnReleasePoolObject(string poolType, GameObject obj)
        {
            _listCache = (PoolTypes)Enum.Parse(typeof(PoolTypes), poolType);
            PoolManager.Instance.ReturnObject(obj,poolType.ToString());
        }
        
        #region Pool Initialization
        
        private void InitPool()
        {
            foreach (var variable in _poolData.PoolTypeDatas)
            {

                _listCache = variable.Key;
                PoolManager.Instance.AddObjectPool<GameObject>(FabricateGameObject, TurnOnGameObject, TurnOffGameObject,
                    variable.Key.ToString(), variable.Value.ObjectLimit, true);
            }
        }
        
        private void TurnOnGameObject(GameObject gameObject)
        {
            gameObject.transform.localPosition = _objTransformCache.position;
            gameObject.SetActive(true);
        }
        
        private void TurnOffGameObject(GameObject gameObject)
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.SetParent(_poolGroup[_listCache].transform);
            gameObject.SetActive(false);
        }
        
        private GameObject FabricateGameObject()
        {
            return Instantiate(_poolData.PoolTypeDatas[_listCache].PooledGameObject,Vector3.zero,
                _poolData.PoolTypeDatas[_listCache].PooledGameObject.transform.rotation,_poolGroup[_listCache].transform);
        }
        
        #endregion
    }
}
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class HostageStackManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public ItemAddOnStackCommand ItemAddOnStackCommand;

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables
        
        private List<GameObject> hostageGameObjects = new List<GameObject>();
        private HostageStackData _hostageStackData;
        private GameObject _playerGameObject;
        private float _maxDistance = 2f;
        private float _speed = 2f;

        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _hostageStackData = GetHostageData();
        }
        
        private HostageStackData GetHostageData() => Resources.Load<CD_HostageStackData>("Data/CD_HostageStackData").HostageStackData;
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        #region Event Subscriptions
        
        private void SubscribeEvents()
        {
            StackSignals.Instance.onAddHostageToStack += OnAddHostageToStack;
            StackSignals.Instance.onPlayerGameObject += OnSetPlayer;
            StackSignals.Instance.onRemoveHostageFromStack += OnRemoveHostageFromStack;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddHostageToStack -= OnAddHostageToStack;
            StackSignals.Instance.onPlayerGameObject -= OnSetPlayer;
            StackSignals.Instance.onRemoveHostageFromStack -= OnRemoveHostageFromStack;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnAddHostageToStack(GameObject arg0)
        {
            hostageGameObjects.Add(arg0);
        }

        private void Update()
        {
            Move();
        }
        
        private void OnSetPlayer(GameObject arg0)
        {
            _playerGameObject = arg0;
        }

        private void Move()
        {
            if (hostageGameObjects.Count<=0)
            {
                return;
            }
            for (int i = 0; i < hostageGameObjects.Count; i++)
            {
                if (i == 0)
                {
                    var actualDistance = Vector3.Distance(hostageGameObjects[i].transform.position,
                        _playerGameObject.transform.position);
                    if (!(actualDistance > _maxDistance)) continue;
                    var position = _playerGameObject.transform.position;
                    var followToCurrent =
                        (hostageGameObjects[i].transform.position - position).normalized;
                    followToCurrent.Scale(new Vector3(_maxDistance, _maxDistance, _maxDistance));
                    hostageGameObjects[i].transform.position = position + followToCurrent;
                    
                    hostageGameObjects[i].transform.LookAt(_playerGameObject.transform);
                }
                else
                {
                    var actualDistance = Vector3.Distance(hostageGameObjects[i].transform.position,
                        hostageGameObjects[i-1].transform.position);
                    if (actualDistance > _maxDistance)
                    {
                        var followToCurrent =
                            (hostageGameObjects[i].transform.position -hostageGameObjects[i-1].transform.position ).normalized;
                        followToCurrent.Scale(new Vector3(_maxDistance, _maxDistance, _maxDistance));
                        hostageGameObjects[i].transform.position = hostageGameObjects[i-1].transform.position + followToCurrent;
                    }
                    hostageGameObjects[i].transform.LookAt(hostageGameObjects[i-1].transform);
                }
            }
        }

        private void OnRemoveHostageFromStack(GameObject hostage)
        {
            hostageGameObjects.Remove(hostage);
            hostageGameObjects.TrimExcess();
        }

    }
}
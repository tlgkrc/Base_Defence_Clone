using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Signals;
using Data.UnityObject;
using Data.ValueObject;
using Commands;
using DG.Tweening;
using Enums;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        public List<GameObject> CollectableStack = new List<GameObject>();
        public List<GameObject> UnstackList = new List<GameObject>();
        public ItemAddOnStackCommand ItemAddOnStack;

        #endregion

        #region Seralized Veriables
        [SerializeField] private GameObject levelHolder;
        [SerializeField] private GameObject collectable;
        #endregion

        #region Private Variables

        private StackData _stackData;
        private StackMoveController _stackMoveController;
        private ItemRemoveOnStackCommand _itemRemoveOnStackCommand;
        private InitialzeStackCommand _initialzeStackCommand;
        private GameObject _playerGameObject;
        private Transform _poolTriggerTransform;

        private bool _isPlayerOnDronePool = false;
        private Vector3 _direction;

        #endregion
        #endregion
        
        private void Awake()
        {
            _stackData = GetStackData();
            Init();
        }
        
        private StackData GetStackData() => Resources.Load<CD_Stack>("Data/CD_StackData").StackData;
        
        private void Init()
        {
            _stackMoveController = new StackMoveController();
            _stackMoveController.InisializedController(_stackData);
            ItemAddOnStack = new ItemAddOnStackCommand(ref CollectableStack, transform, _stackData);
            _itemRemoveOnStackCommand = new ItemRemoveOnStackCommand(ref CollectableStack, ref levelHolder);
            _initialzeStackCommand = new InitialzeStackCommand(collectable, this);
        }

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvent();
            _initialzeStackCommand.Execute(_stackData.InitialStackItem);
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            StackSignals.Instance.onInteractionCollectable += OnInteractionWithCollectable;
            StackSignals.Instance.onInteractionObstacle += _itemRemoveOnStackCommand.Execute;
            StackSignals.Instance.onPlayerGameObject += OnSetPlayer;
            StackSignals.Instance.onGetCurrentScore += OnGetStackCount;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        }
        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            StackSignals.Instance.onInteractionCollectable -= OnInteractionWithCollectable;
            StackSignals.Instance.onInteractionObstacle -= _itemRemoveOnStackCommand.Execute;
            StackSignals.Instance.onPlayerGameObject -= OnSetPlayer;
            StackSignals.Instance.onGetCurrentScore -= OnGetStackCount;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        }
        private void OnDisable()
        {
            UnSubscribeEvent();
        }
        #endregion

        private void Start()
        {
            ScoreSignals.Instance.onSetScore?.Invoke(CollectableStack.Count);
        }
        
        private void Update()
        {
            if (_isPlayerOnDronePool)StackMove(true);
            else StackMove();
        }

        private void OnSetPlayer(GameObject player)
        {
            _playerGameObject = player;
        }
        
        private void StackMove(bool isOnDronePool=false)
        {
             if (gameObject.transform.childCount > 0)
             {
                _stackMoveController.StackItemsMoveOrigin(_playerGameObject.transform.position, CollectableStack,isOnDronePool);
             }
        }
        
        private void OnInteractionWithCollectable(GameObject collectableGameObject)
        {
            ItemAddOnStack.Execute(collectableGameObject);
            collectableGameObject.tag = "Collected";
        }

        private void OnPlayerCollideWithDronePool(Transform poolTriggerTransform)
        {
            _poolTriggerTransform = poolTriggerTransform;
            _isPlayerOnDronePool = true;
            CollectableStack[0].transform.DOMoveZ(CollectableStack[0].transform.position.z + 5, 1f);
        }
        

        private void OnReset()
        {
            foreach (Transform childs in transform)
            {
                Destroy(childs.gameObject);
            }
            CollectableStack.Clear();
            _initialzeStackCommand.Execute(_stackData.InitialStackItem);
        }

        private void OnStackToUnstack(GameObject collectable)//command olabilir
        {
            UnstackList.Add(collectable);
            collectable.transform.SetParent(levelHolder.transform);
            CollectableStack.Remove(collectable);
            CollectableStack.TrimExcess();
        }
        
        
        
        
        private int OnGetStackCount()
        {
            return CollectableStack.Count;
        }

        private void OnLevelSuccessful()
        {
            var lastCollectable = CollectableStack[CollectableStack.Count - 1];
            var itemDuration = 1;
            foreach (var item in CollectableStack)
            {
                item.transform.SetParent(levelHolder.transform);
                item.transform.DOMove(_playerGameObject.transform.position, .1f*itemDuration).OnComplete(()=>
                {
                    if (lastCollectable.Equals(item))
                    {
                        StackSignals.Instance.onLastCollectableAddedToPlayer?.Invoke(true);
                    }
                    item.SetActive(false);
                    StackSignals.Instance.onSetPlayerScale?.Invoke(.1f);

                  
                });
                itemDuration += 1;
                
            }
            CollectableStack.Clear();
            CollectableStack.TrimExcess();
        }
    }
}
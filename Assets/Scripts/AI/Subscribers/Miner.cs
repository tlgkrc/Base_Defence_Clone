using System;
using System.Collections.Generic;
using AI.States;
using AI.States.Miner;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Subscribers
{
    public class Miner : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public Transform Target { get; set; }

        #endregion

        #region Serialized Variables
        
        [SerializeField] private GameObject gem;
        [SerializeField] private GameObject fakeGem;
        [SerializeField] private GameObject pickAxe;
        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables

        
        private float _harvestTime = 3.5f;
        private float _passedTime;
        [ShowInInspector]private Transform _gemStock;
        [ShowInInspector]private List<Transform> _mineTransforms = new List<Transform>();
        private AIStateMachine _aiStateMachine;

        #endregion

        #endregion

        private void Init()
        {
            _aiStateMachine = new AIStateMachine();
            
            var navMeshAgent = GetComponent<NavMeshAgent>();
            var navMeshObstacle = GetComponent<NavMeshObstacle>();
            var search = new SearchForGem(this, _mineTransforms);
            var moveToSelected = new MoveToSelectedMine(this, navMeshAgent, animator, navMeshObstacle);
            var dig = new DigForGem(this, animator, navMeshAgent, navMeshObstacle);
            var harvest = new HarvestGem(this);
            var returnToGemStock = new ReturnToGemStock(this, _gemStock, navMeshAgent, animator);
            var placeGemInStockPile = new PlaceGemToStock(this);

            At(search, moveToSelected, HasTarget());
            At(moveToSelected, dig, ReachedResource());
            At(dig, harvest, TimeIsPassed());
            At(harvest, returnToGemStock, StockIsFull());
            At(returnToGemStock, placeGemInStockPile, ReachedStockPile());
            At(placeGemInStockPile, search, DeliveredGem());

            _aiStateMachine.SetState(search);

            Func<bool> HasTarget() => () => Target != null;
            Func<bool> ReachedResource() => () =>
                Target != null && Vector3.Distance(transform.position, Target.transform.position) < 1.01f;
            Func<bool> TimeIsPassed() => () =>
            {
                _passedTime += Time.deltaTime;
                
                if (_passedTime >= _harvestTime)
                {
                    _passedTime = 0;
                    return true;
                }
                
                return false;
            };
            Func<bool> StockIsFull() => () => true;
            Func<bool> ReachedStockPile() => () =>
                _gemStock != null && Vector3.Distance(transform.position, _gemStock.transform.position) < 1f;
            Func<bool> DeliveredGem() => () => true;
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            GetReferences();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }

        private void UnsubscribeEvents()
        {

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            Init();
        }
        
        private void At(IAIStates to, IAIStates from, Func<bool> condition) =>
            _aiStateMachine.AddTransition(to, from, condition);

        private void GetReferences()
        {
            _gemStock = BaseSignals.Instance.onSetGemStock?.Invoke();
            var transforms = AISignals.Instance.onSetMineTransforms?.Invoke();
            foreach (var value in transforms)
            {
                _mineTransforms.Add(value);
            }
        }

        private void Update()
        {
            _aiStateMachine.Tick();
        }

        public void HoldGem(bool isHold)
        {
            fakeGem.SetActive(isHold);
        }

        public void HoldPickAxe(bool isHold)
        {
            pickAxe.SetActive(isHold);
        }

        public void AddGemToStock()
        {
            var gO = PoolSignals.Instance.onGetPoolObject(gem.name, transform);
            StackSignals.Instance.onAddStack?.Invoke(_gemStock.GetInstanceID(),gO);
        }
    }
}
using System;
using System.Collections.Generic;
using AI.States;
using AI.States.Miner;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Subscribers
{
    public class Miner : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public Transform Target { get; set; }
        //use mediator pattern for that miner move to which mine,mine will be send as a data ,miners send mine to mediator, 

        #endregion

        #region Serialized Variables

        [SerializeField] private List<Transform> mineTransforms;
        [SerializeField] private GameObject gem;
        [SerializeField] private Transform gemStock;

        #endregion

        #region Private Variables

        private AIStateMachine _aiStateMachine;
        private float _harvestTime = 2.5f;
        private float _passedTime;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            var navMeshAgent = GetComponent<NavMeshAgent>();

            _aiStateMachine = new AIStateMachine();

            var search = new SearchForGem(this,mineTransforms);
            var moveToSelected = new MoveToSelectedMine(this, navMeshAgent);
            var dig = new DigForGem(this);
            var harvest = new HarvestGem(this);
            var returnToGemStock = new ReturnToGemStock(this,gemStock, navMeshAgent);
            var placeGemInStockPile = new PlaceGemToStock(this);
            
            At(search,moveToSelected,HasTarget());
            At(moveToSelected,dig,ReachedResource());
            At(dig,harvest,TimeIsPassed());
            At(harvest, returnToGemStock, StockIsFull());
            At(returnToGemStock, placeGemInStockPile, ReachedStockPile());
            At(placeGemInStockPile, search, DeliveredGem());

            _aiStateMachine.SetState(search);

            Func<bool> HasTarget() =>()=> Target != null;
            Func<bool> ReachedResource() => () =>
                Target != null && Vector3.Distance(transform.position, Target.transform.position)<1.1f;
            Func<bool> TimeIsPassed() => () =>
            {
                _passedTime += Time.deltaTime;
                if (_passedTime >= _harvestTime )
                {
                    _passedTime = 0;
                    return true;
                }
                return false;
            };
            Func<bool> StockIsFull() => () => true;
            Func<bool> ReachedStockPile() => () =>
                gemStock != null && Vector3.Distance(transform.position, gemStock.transform.position) < 1f;
            Func<bool> DeliveredGem() => () => true;
        }
        
        private void At(IAIStates to, IAIStates from, Func<bool> condition) =>
            _aiStateMachine.AddTransition(to, from, condition);

        public void TakeFromMine()
        {
            //take from pool by func ,
        }

        private void Update()
        {
            _aiStateMachine.Tick();
        }

        public void DropCollectedGem()
        {
            //put to stack ,but before that instantiate a stack 
        }

        public void AddGemToStock()
        {
            StackSignals.Instance.onAddStack?.Invoke(gemStock.GetInstanceID());
        }
    }
}
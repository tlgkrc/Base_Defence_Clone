using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace ExampleAIStateMachine.Scripts
{

    public class Collector : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public event Action<int> OnCollectorChanged;
        public CollectableResources Target { get; set; }
        public StocksPile StocksPile { get; set; }

        #endregion

        #region Serialized Variables

        [SerializeField] private int _maxCarried = 5;

        #endregion

        #region Private Variables

        private AIStateMachine _aiStateMachine;
        [ShowInInspector]private int _collected;

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

            var search = new SearchForResources(this);
            var moveToSelected = new MoveToSelectedResources(this, navMeshAgent);
            var harvest = new HarvestResources(this);
            var returnToStockPile = new ReturnToStockPile(this, navMeshAgent);
            var placeResourcesInStockPile = new PlaceResourcesInStockPile(this);
            
            At(search,moveToSelected,HasTarget());
            At(moveToSelected, search, StuckForOverASecond());
            At(moveToSelected,harvest,ReachedResource());
            At(harvest, search, TargetIsDepletedAndCanCarryMore());
            At(harvest, returnToStockPile, InventoryFull());
            At(returnToStockPile, placeResourcesInStockPile, ReachedStockPile());
            At(placeResourcesInStockPile,search,()=>_collected==0);

            _aiStateMachine.SetState(search);


            Func<bool> HasTarget() =>()=> Target != null;
            Func<bool> StuckForOverASecond() => () => moveToSelected.TimeStuck > 1f;
            Func<bool> ReachedResource() => () =>
                Target != null && Vector3.Distance(transform.position, Target.transform.position)<1f;
            Func<bool> InventoryFull() => () => _collected >= _maxCarried;
            Func<bool> TargetIsDepletedAndCanCarryMore() => () => (Target == null || Target.IsDepleted) &&!InventoryFull().Invoke();
            Func<bool> ReachedStockPile() => () =>
                StocksPile != null && Vector3.Distance(transform.position, StocksPile.transform.position) < 1f;
        }

        private void At(IAIState to, IAIState from, Func<bool> condition) =>
            _aiStateMachine.AddTransition(to, from, condition);

        private void Update() => _aiStateMachine.Tick();

        public void TakeFromTarget()
        {
            if (Target.Take())
            {
                _collected++;
                OnCollectorChanged?.Invoke(_collected);
            }
        }

        public bool Take()
        {
            if (_collected <= 0)
            {
                return false;
            }

            _collected--;
            OnCollectorChanged?.Invoke(_collected);
            return true;
        }

        public void DropAllResources()
        {
            if (_collected > 0)
            {
                FindObjectOfType<StuffDropper>().Drop(_collected, transform.position);
                _collected = 0;
                OnCollectorChanged?.Invoke(_collected);
            }
        }
    }
}
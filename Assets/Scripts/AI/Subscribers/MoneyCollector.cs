using System;
using AI.Controllers;
using AI.States;
using AI.States.MoneyCollector;
using Data.UnityObject;
using Data.ValueObject;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Subscribers
{
    public class MoneyCollector : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public Transform Target { get; set; }
        public bool CollectorInBase { get; set; }

        #endregion

        #region Serialized Variables

        [SerializeField] private MoneyCollectorPhysicController physicController;
        [SerializeField] private StackManager stackManager;
        [SerializeField] private Transform baseTransform;
        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables

        private bool _targetInCollider;
        private AIStateMachine _aiStateMachine;
        [ShowInInspector] private MoneyCollectorData _moneyCollectorData;

        #endregion

        #endregion

        private void Awake()
        {
            _aiStateMachine = new AIStateMachine();
            _moneyCollectorData = GetMoneyCollectorData();
            
            var navMeshAgent = GetComponent<NavMeshAgent>();
            var searchMoney = new SearchClosestMoney(this,animator);
            var moveToMoney = new MoveToTargetMoney(this,navMeshAgent,animator);
            var takeMoney = new TakeMoney(this);
            var moveBase = new MoveToBase(this,navMeshAgent);
            var deliverMoney = new DeliverMoney(this);

            At(searchMoney, moveToMoney, HasTarget());
            At(moveToMoney,takeMoney,ArrivePosition());
            At(takeMoney,searchMoney,HasMoney());
            
            _aiStateMachine.AddAnyTransition(moveBase,StackIsFull());
            
            At(moveBase,deliverMoney,IsInBase());
            At(deliverMoney,searchMoney,StackIsEmpty());
            
            _aiStateMachine.SetState(searchMoney);

            Func<bool> HasTarget() => () => Target != null;
            Func<bool> ArrivePosition() => () => Target != null && Vector3.Distance(transform.position, Target.position) <= 2f;
            Func<bool> HasMoney() => () => IsInStack() && _moneyCollectorData.MaxStackCount > stackManager.transform.childCount;
            Func<bool> StackIsFull() => () => _moneyCollectorData.MaxStackCount == stackManager.transform.childCount && !CollectorInBase;
            Func<bool> IsInBase() => () => CollectorInBase && Vector3.Distance(transform.position, baseTransform.position) <= 2f ;
            Func<bool> StackIsEmpty() => () => stackManager.transform.childCount == 0;
            
            navMeshAgent.speed = _moneyCollectorData.Speed;
        }
        
        private void At(IAIStates to, IAIStates from, Func<bool> condition)
        {
            _aiStateMachine.AddTransition(to, from, condition);
        }

        private MoneyCollectorData GetMoneyCollectorData()
        {
            return Resources.Load<CD_MoneyCollectorData>("Data/CD_MoneyCollectorData").MoneyCollectorData;
        }

        private void Update()
        {
           _aiStateMachine.Tick();
        }
        
        public void SearchMoney()
        {
            physicController.IncreaseColliderRadius();
        }

        public void ResetRadius()
        {
            physicController.ResetColliderRadius();
        }

        public void TakeMoneyToStack()
        {
            StackSignals.Instance.onAddStack?.Invoke(transform.GetInstanceID(),Target.gameObject);
            CollectorInBase = false;
        }

        public void SetTargetTransformToBase()
        {
            Target = baseTransform;
            CollectorInBase = true;
        }

        private bool IsInStack()
        {
            if (Target != null)
            {
               return Target.parent ==stackManager.transform; 
            }
            return false;
        }

        public void DeliverMoney()
        {
            StackSignals.Instance.onClearDynamicStack?.Invoke(stackManager.GetInstanceID());
        }

        public void ResetGrid()
        {
        }
    }
}
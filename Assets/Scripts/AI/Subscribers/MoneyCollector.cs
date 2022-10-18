using System;
using AI.States;
using AI.States.MoneyCollector;
using Data.UnityObject;
using Data.ValueObject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AI.Subscribers
{
    public class MoneyCollector : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public Transform Target { get; set; }

        #endregion

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        private AIStateMachine _aiStateMachine;
        [ShowInInspector]private MoneyCollectorData _moneyCollectorData;
        private bool _targetInCollider;

        #endregion

        #endregion

        private void Awake()
        {
            _moneyCollectorData = GetMoneyCollectorData();
            var searchMoney = new SearchClosestMoney(this);
            var moveToMoney = new MoveToTargetMoney(this);
            var checkMoney = new CheckMoney(this);
            var takeMoney = new TakeMoney(this);

            At(searchMoney, moveToMoney, HasTarget());
            At(moveToMoney,checkMoney,ArrivePosition());
            At(checkMoney,takeMoney,ExistMoney());
            At(takeMoney,searchMoney,HasMoney());

            _aiStateMachine.SetState(searchMoney);

            Func<bool> HasTarget() => () => Target != null;
            Func<bool> ArrivePosition() => () => Vector3.Distance(transform.position, Target.position) <= 2f;
            Func<bool> ExistMoney() => () => _targetInCollider == true && Target != null;
            Func<bool> HasMoney() => () => Target == null;
        }
        
        private void At(IAIStates to, IAIStates from, Func<bool> condition)
        {
            _aiStateMachine.AddTransition(to, from, condition);
        }

        private MoneyCollectorData GetMoneyCollectorData()
        {
            return Resources.Load<CD_MoneyCollectorData>("Data/CD_MoneyCollectorData").MoneyCollectorData;
        }
    }
}
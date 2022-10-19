﻿using UnityEngine.AI;

namespace AI.States.MoneyCollector
{
    public class MoveToTargetMoney: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.MoneyCollector _moneyCollector;
        private readonly NavMeshAgent _navMeshAgent;

        #endregion

        #endregion

        public MoveToTargetMoney(Subscribers.MoneyCollector moneyCollector,NavMeshAgent navMeshAgent)
        {
            _moneyCollector = moneyCollector;
            _navMeshAgent = navMeshAgent;
        }
        public void Tick()
        {
        }

        public void OnEnter()
        {
            _navMeshAgent.SetDestination(_moneyCollector.Target.position);
            _moneyCollector.CollectorInBase = false;
        }

        public void OnExit()
        {
        }
    }
}
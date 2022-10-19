using UnityEngine;
using UnityEngine.AI;

namespace AI.States.MoneyCollector
{
    public class MoveToBase: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.MoneyCollector _moneyCollector;
        private readonly NavMeshAgent _navMeshAgent;

        #endregion

        #endregion

        public MoveToBase(Subscribers.MoneyCollector moneyCollector,NavMeshAgent navMeshAgent)
        {
            _moneyCollector = moneyCollector;
            _navMeshAgent = navMeshAgent;
        }
        public void Tick()
        {
        }

        public void OnEnter()
        {
            _moneyCollector.SetTargetTransformToBase();
            _navMeshAgent.SetDestination(_moneyCollector.Target.position);
        }

        public void OnExit()
        {
        }
    }
}
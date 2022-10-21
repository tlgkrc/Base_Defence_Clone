using Enums.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States.MoneyCollector
{
    public class MoveToTargetMoney: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.MoneyCollector _moneyCollector;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;

        #endregion

        #endregion

        public MoveToTargetMoney(Subscribers.MoneyCollector moneyCollector,NavMeshAgent navMeshAgent,Animator animator)
        {
            _moneyCollector = moneyCollector;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }
        public void Tick()
        {
        }

        public void OnEnter()
        {
            _navMeshAgent.SetDestination(_moneyCollector.Target.position);
            _moneyCollector.CollectorInBase = false;
            _animator.SetTrigger(MoneyWorkerAnimTypes.Walk.ToString());
        }

        public void OnExit()
        {
        }
    }
}
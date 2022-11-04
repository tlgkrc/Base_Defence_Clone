using Enums.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States.AmmoSupplier
{
    public class MoveToAmmoDepot: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Transform _ammoDepotTransform;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Subscribers.AmmoSupplier _ammoSupplier;

        #endregion

        #endregion

        public MoveToAmmoDepot(Subscribers.AmmoSupplier ammoSupplier,NavMeshAgent navMeshAgent,Transform ammoDepotTransform,Animator animator)
        {
            _ammoSupplier = ammoSupplier;
            _navMeshAgent = navMeshAgent;
            _ammoDepotTransform = ammoDepotTransform;
            _animator = animator;
        }

        public void Tick()
        {
            _navMeshAgent.SetDestination(_ammoDepotTransform.position);
        }

        public void OnEnter()
        {
            _animator.SetTrigger(AmmoWorkerAnimStates.Walk.ToString());
        }

        public void OnExit()
        {
        }
    }
}
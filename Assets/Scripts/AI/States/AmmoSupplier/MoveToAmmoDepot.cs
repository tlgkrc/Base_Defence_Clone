using Enums.Animations;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States.AmmoSupplier
{
    public class MoveToAmmoDepot: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.AmmoSupplier _ammoSupplier;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Transform _ammoDepotTransform;
        private readonly Animator _animator;


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
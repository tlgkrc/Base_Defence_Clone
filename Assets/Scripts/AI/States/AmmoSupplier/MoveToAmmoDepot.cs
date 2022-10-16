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

        #endregion

        #endregion

        public MoveToAmmoDepot(Subscribers.AmmoSupplier ammoSupplier,NavMeshAgent navMeshAgent,Transform ammoDepotTransform)
        {
            _ammoSupplier = ammoSupplier;
            _navMeshAgent = navMeshAgent;
            _ammoDepotTransform = ammoDepotTransform;
        }

        public void Tick()
        {
            _navMeshAgent.SetDestination(_ammoDepotTransform.position);
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}
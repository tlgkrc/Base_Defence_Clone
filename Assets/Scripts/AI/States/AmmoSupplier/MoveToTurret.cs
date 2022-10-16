using UnityEngine.AI;

namespace AI.States.AmmoSupplier
{
    public class MoveToTurret: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private Subscribers.AmmoSupplier _ammoSupplier;
        private NavMeshAgent _navMeshAgent;

        #endregion

        #endregion

        public MoveToTurret(Subscribers.AmmoSupplier ammoSupplier, NavMeshAgent navMeshAgent)
        {
            _ammoSupplier = ammoSupplier;
            _navMeshAgent = navMeshAgent;
        }

        public void Tick()
        {
            _navMeshAgent.SetDestination(_ammoSupplier.Target.position);
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}
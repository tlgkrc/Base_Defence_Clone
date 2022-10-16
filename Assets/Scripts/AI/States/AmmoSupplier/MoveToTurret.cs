using UnityEngine;
using UnityEngine.AI;

namespace AI.States.AmmoSupplier
{
    public class MoveToTurret: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private Subscribers.AmmoSupplier _ammoSupplier;
        private NavMeshAgent _navMeshAgent;
        private Vector3 _lastPosition;
        private float _timeStuck;

        #endregion

        #endregion

        public MoveToTurret(Subscribers.AmmoSupplier ammoSupplier, NavMeshAgent navMeshAgent)
        {
            _ammoSupplier = ammoSupplier;
            _navMeshAgent = navMeshAgent;
        }

        public void Tick()
        {
            if (Vector3.Distance(_ammoSupplier.transform.position,_lastPosition)<=1f)
            {
                _timeStuck += Time.time;
            }
            _lastPosition = _ammoSupplier.transform.position;
        }

        public void OnEnter()
        {
            _timeStuck = 0f;
            _navMeshAgent.SetDestination(_ammoSupplier.Target.transform.position);
        }

        public void OnExit()
        {

        }
    }
}
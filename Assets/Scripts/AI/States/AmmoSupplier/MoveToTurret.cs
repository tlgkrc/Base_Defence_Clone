using Enums.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States.AmmoSupplier
{
    public class MoveToTurret: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.AmmoSupplier _ammoSupplier;
        private readonly NavMeshAgent _navMeshAgent;
        private Vector3 _lastPosition;
        private float _timeStuck;
        private readonly Animator _animator;

        #endregion

        #endregion

        public MoveToTurret(Subscribers.AmmoSupplier ammoSupplier, NavMeshAgent navMeshAgent,Animator animator)
        {
            _ammoSupplier = ammoSupplier;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
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
            _navMeshAgent.SetDestination(_ammoSupplier.Target.position);
            _animator.SetTrigger(AmmoWorkerAnimStates.Walk.ToString());
        }

        public void OnExit()
        {
            
        }
    }
}
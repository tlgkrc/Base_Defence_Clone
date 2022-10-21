using Enums.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States.Miner
{
    public class DigForGem: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.Miner _miner;
        private float _nextTakeResourcesTime;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly NavMeshObstacle _navMeshObstacle;

        #endregion

        #endregion
        public DigForGem(Subscribers.Miner miner,Animator animator,NavMeshAgent navMeshAgent,NavMeshObstacle navMeshObstacle)
        {
            _miner = miner;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _navMeshObstacle = navMeshObstacle;
        }
        public void Tick()
        {
            if (_miner.Target !=null)
            {
                if (_nextTakeResourcesTime <=Time.time)
                {
                    _nextTakeResourcesTime = Time.time ;
                }
            }
        }

        public void OnEnter()
        {
            _animator.SetTrigger(GemWorkerAnimTypes.Dig.ToString());
        }

        public void OnExit()
        {
            _miner.HoldPickAxe(false);
            _navMeshObstacle.enabled = false;
            _navMeshAgent.enabled = true;
        }
    }
}
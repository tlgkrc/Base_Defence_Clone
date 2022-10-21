using Enums.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States.Miner
{
    public class MoveToSelectedMine: IAIStates
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Private Variables

        private readonly Subscribers.Miner _miner;
        private readonly NavMeshAgent _agent;
        private readonly NavMeshObstacle _navMeshObstacle;
        private Vector3 _lastPosition;
        private float _timeStuck;
        private readonly Animator _animator;

        #endregion

        #endregion

        public MoveToSelectedMine(Subscribers.Miner miner,NavMeshAgent agent,Animator animator,NavMeshObstacle navMeshObstacle)
        {
            _miner = miner;
            _agent = agent;
            _animator = animator;
            _navMeshObstacle = navMeshObstacle;
        }
        public void Tick()
        {
            if (Vector3.Distance(_miner.transform.position,_lastPosition)<=1f)
            {
                _timeStuck += Time.time;
            }
            _lastPosition = _miner.transform.position;
        }

        public void OnEnter()
        {
            _timeStuck = 0f;
            _agent.SetDestination(_miner.Target.transform.position);
            _animator.SetTrigger(GemWorkerAnimTypes.Walk.ToString());
        }

        public void OnExit()
        {
            _agent.enabled = false;
            _navMeshObstacle.enabled = true;
            _miner.HoldPickAxe(true);
        }
    }
}
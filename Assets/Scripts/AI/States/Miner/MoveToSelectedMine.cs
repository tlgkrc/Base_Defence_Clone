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
        private Vector3 _lastPosition;
        private float _timeStuck;

        #endregion

        #endregion

        public MoveToSelectedMine(Subscribers.Miner miner,NavMeshAgent agent)
        {
            _miner = miner;
            _agent = agent;
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
        }

        public void OnExit()
        {
            
        }
    }
}
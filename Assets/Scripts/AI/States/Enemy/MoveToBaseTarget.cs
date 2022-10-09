using UnityEngine;
using UnityEngine.AI;

namespace AI.States.Enemy
{
    public class MoveToBaseTarget: IAIStates
    {
        #region Self Variables

        #region Public Variables
        
        

        #endregion

        #region Private Variables

        private Subscribers.Enemy _enemy;
        private NavMeshAgent _agent;
        private float _timeStuck;
        private Vector3 _lastPosition;

        #endregion

        #endregion

        public MoveToBaseTarget(Subscribers.Enemy enemy,NavMeshAgent agent)
        {
            _enemy = enemy;
            _agent = agent;
        }

        public void Tick()
        {
            if (Vector3.Distance(_enemy.transform.position,_lastPosition)<=1f)
            {
                _timeStuck += Time.time;
            }
            _lastPosition = _enemy.transform.position;
        }

        public void OnEnter()
        {
            _timeStuck = 0;
            _agent.SetDestination(_enemy.BaseTarget.transform.position);
        }

        public void OnExit()
        {
            
        }
    }
}
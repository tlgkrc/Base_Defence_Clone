using Data.ValueObject;
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

        private readonly Subscribers.Enemy _enemy;
        private readonly NavMeshAgent _agent;
        private readonly EnemyGOData _enemyGoData;
        private float _timeStuck;
        private Vector3 _lastPosition;
        private Animator _animator;

        #endregion

        #endregion

        public MoveToBaseTarget(Subscribers.Enemy enemy,NavMeshAgent agent,EnemyGOData enemyGoData,Animator animator)
        {
            _enemy = enemy;
            _agent = agent;
            _enemyGoData = enemyGoData;
            _animator = animator;
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
            _agent.speed = _enemyGoData.Speed;
            _agent.SetDestination(_enemy.Target.position);
            _animator.SetBool("IsStop" , false);
        }

        public void OnExit()
        {
            
        }
    }
}
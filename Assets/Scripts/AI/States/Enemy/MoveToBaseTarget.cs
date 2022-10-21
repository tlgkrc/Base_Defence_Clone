using Data.ValueObject;
using Enums.Animations;
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
        private readonly Animator _animator;

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
            _timeStuck += Time.deltaTime;
            if (_timeStuck is < 1.1f or > 1.9f and < 3.2f)
            {
                _agent.speed = 0;
            }
            else if ((_timeStuck is >= 1.1f and <= 1.9f) || (_timeStuck>= 3.2 && _timeStuck<= 4.033))
            {
                _agent.speed = _enemyGoData.Speed;
            }
            else
            {
                _timeStuck = 0;
            }
        }

        public void OnEnter()
        {
            _timeStuck = 0;
            _agent.speed = _enemyGoData.Speed;
            _agent.SetDestination(_enemy.Target.position);
            _animator.SetTrigger(EnemyAnimTypes.Walk.ToString());
        }

        public void OnExit()
        {
        }
    }
}
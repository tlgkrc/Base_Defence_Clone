using Data.ValueObject;
using Enums;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States.Enemy
{
    public class MoveToPlayer: IAIStates
    {
        #region Self Variables

        #region Public Variables
        
        

        #endregion

        #region Private Variables

        private readonly Subscribers.Enemy _enemy;
        private readonly NavMeshAgent _agent;
        private readonly EnemyGOData _enemyGoData;
        private readonly Animator _animator;

        #endregion

        #endregion

        public MoveToPlayer(Subscribers.Enemy enemy, NavMeshAgent agent, EnemyGOData enemyGoData, Animator animator)
        {
            _enemy = enemy;
            _agent = agent;
            _enemyGoData = enemyGoData;
            _animator = animator;
        }

        public void Tick()
        {
            _agent.SetDestination(_enemy.Target.position);
        }

        public void OnEnter()
        {
            _agent.speed = _enemyGoData.IncreasedSpeed;
            _animator.SetTrigger(EnemyAnimTypes.Run.ToString());
        }

        public void OnExit()
        {
            _agent.speed = _enemyGoData.Speed;
        }
    }
}
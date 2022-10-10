using Data.ValueObject;
using UnityEngine.AI;

namespace AI.States.Enemy
{
    public class MoveToPlayer: IAIStates
    {
        #region Self Variables

        #region Public Variables
        
        

        #endregion

        #region Private Variables

        private Subscribers.Enemy _enemy;
        private NavMeshAgent _agent;
        private EnemyGOData _enemyGoData;

        #endregion

        #endregion

        public MoveToPlayer(Subscribers.Enemy enemy,NavMeshAgent agent,EnemyGOData enemyGoData)
        {
            _enemy = enemy;
            _agent = agent;
            _enemyGoData = enemyGoData;
        }

        public void Tick()
        {
            _agent.SetDestination(_enemy.Target.position);
        }

        public void OnEnter()
        {
            _agent.speed = _enemyGoData.IncreasedSpeed;
        }

        public void OnExit()
        {
            _agent.speed = _enemyGoData.Speed;
        }
    }
}
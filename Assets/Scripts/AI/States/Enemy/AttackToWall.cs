using Enums.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States.Enemy
{
    public class AttackToWall: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;
        private readonly Subscribers.Enemy _enemy;
        
        #endregion

        #endregion

        public AttackToWall(Subscribers.Enemy enemy,NavMeshAgent agent,Animator animator)
        {
            _enemy = enemy;
            _agent = agent;
            _animator = animator;
        }
        
        public void Tick()
        {
        }

        public void OnEnter()
        {
            _agent.enabled = false;
            _animator.SetTrigger(EnemyAnimTypes.Attack.ToString());
        }

        public void OnExit()
        {
            _agent.enabled = true;
        }
    }
}
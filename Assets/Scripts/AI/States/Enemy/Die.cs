using UnityEngine;
using UnityEngine.AI;

namespace AI.States.Enemy
{
    public class Die: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private Animator _animator;
        private readonly Subscribers.Enemy _enemy;
        private readonly NavMeshAgent _agent;
        
        #endregion

        #endregion

        public Die(Subscribers.Enemy enemy,Animator animator,NavMeshAgent agent)
        {
            _enemy = enemy;
            _animator = animator;
            _agent = agent;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _enemy.Target = null;
            _agent.enabled = false;
        }

        public void OnExit()
        {
        }
    }
}
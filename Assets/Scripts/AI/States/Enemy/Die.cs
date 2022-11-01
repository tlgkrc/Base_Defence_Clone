using Enums.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States.Enemy
{
    public class Die: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private Subscribers.Enemy _enemy;
        private NavMeshAgent _agent;
        private Animator _animator;

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
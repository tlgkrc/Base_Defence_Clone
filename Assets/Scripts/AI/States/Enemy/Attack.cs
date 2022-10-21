using Enums;
using Enums.Animations;
using UnityEditor.Animations;
using UnityEngine;

namespace AI.States.Enemy
{
    public class Attack: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private Subscribers.Enemy _enemy;
        private readonly Animator _animator;

        #endregion

        #endregion

        public Attack(Subscribers.Enemy enemy,Animator animator)
        {
            _enemy = enemy;
            _animator = animator;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            _animator.SetTrigger(EnemyAnimTypes.Attack.ToString());
        }

        public void OnExit()
        {

        }
    }
}
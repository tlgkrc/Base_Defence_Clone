using Enums;
using Enums.Animations;
using UnityEngine;

namespace AI.States.Enemy
{
    public class AttackToWall: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private Subscribers.Enemy _enemy;
        private Animator _animator;

        #endregion

        #endregion

        public AttackToWall(Subscribers.Enemy enemy,Animator animator)
        {
            _enemy = enemy;
            _animator = animator;
        }
        
        public void Tick()
        {
            //this gameobject is defined as obstacle
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
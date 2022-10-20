using Enums;
using UnityEngine;

namespace AI.States.Enemy
{
    public class SearchForPlayer: IAIStates
    {
        #region Self Variables

        #region Public Variables
        
        

        #endregion

        #region Private Variables

        private Subscribers.Enemy _enemy;
        private readonly Animator _animator;
        private static readonly int Stop = Animator.StringToHash("Stop");

        #endregion

        #endregion

        public SearchForPlayer(Subscribers.Enemy enemy,Animator animator)
        {
            _enemy = enemy;
            _animator = animator;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {

        }
    }
}
using UnityEngine;

namespace AI.States.Enemy
{
    public class SearchForPlayer: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private static readonly int Stop = Animator.StringToHash("Stop");
        private readonly Animator _animator;
        private Subscribers.Enemy _enemy;

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
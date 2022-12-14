using System.Collections.Generic;
using UnityEngine;

namespace AI.States.Enemy
{
    public class SearchForBaseTarget: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Animator _animator;
        private readonly List<Transform> _targetTransforms;
        private readonly Subscribers.Enemy _enemy;

        #endregion

        #endregion

        public SearchForBaseTarget(Subscribers.Enemy enemy,List<Transform> targetTransforms,Animator animator)
        {
            _enemy = enemy;
            _targetTransforms = targetTransforms;
            _animator = animator;
        }

        public void Tick()
        {
            _enemy.Target =  ChooseBaseTarget(_targetTransforms);
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        private Transform ChooseBaseTarget(IReadOnlyList<Transform> transforms)
        {
            var index = Random.Range(0, transforms.Count);
            var target = transforms[index];
            return target;
        }
    }
}
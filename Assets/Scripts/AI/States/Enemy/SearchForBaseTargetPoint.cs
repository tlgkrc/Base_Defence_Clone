using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States.Enemy
{
    public class SearchForTargetPoint: IAIStates
    {
        #region Self Variables

        #region Public Variables
        
        

        #endregion

        #region Private Variables

        private Subscribers.Enemy _enemy;
        private List<Transform> _targetTransforms;

        #endregion

        #endregion

        public SearchForTargetPoint(Subscribers.Enemy enemy,List<Transform> targetTransforms)
        {
            _enemy = enemy;
            _targetTransforms = targetTransforms;
        }

        public void Tick()
        {
           _enemy.BaseTarget =  ChooseBaseTarget(_targetTransforms);
        }

        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        private Transform ChooseBaseTarget(List<Transform> transforms)
        {
            var index = Random.Range(0, transforms.Count);
            var target = transforms[index];
            return target;
        }
    }
}
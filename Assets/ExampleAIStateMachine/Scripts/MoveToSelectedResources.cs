using System;
using UnityEngine;
using UnityEngine.AI;

namespace ExampleAIStateMachine.Scripts
{
    internal class MoveToSelectedResources:IAIState
    {
        #region Self Variables

        #region Public Variables

        public float TimeStuck;

        #endregion

        #region Private Variables

        private readonly Collector _collector;
        private readonly NavMeshAgent _navMeshAgent;
        private static readonly int Speed = 5;
        private Vector3 _lastPosition = Vector3.zero;

        #endregion

        #endregion

        public MoveToSelectedResources(Collector collector,NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
            _collector = collector;
        }
        public void Tick()
        {
            if (Vector3.Distance(_collector.transform.position,_lastPosition)<=0f)
            {
                TimeStuck += Time.deltaTime;
            }

            _lastPosition = _collector.transform.position;
        }

        public void OnEnter()
        {
            TimeStuck = 0f;
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_collector.Target.transform.position);
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
        }
    }
}
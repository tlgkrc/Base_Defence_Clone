using System;
using UnityEngine;

namespace ExampleAIStateMachine.Scripts
{
    internal class HarvestResources:IAIState
    {
        #region Self Variables

        #region Private Variables

        private readonly Collector _collector;
        private float _resourcesPerSecond =3 ;
        private float _nextTakeResourcesTime;

        #endregion

        #endregion

        public HarvestResources(Collector collector)
        {
            _collector = collector;
        }
        public void Tick()
        {
            if (_collector.Target != null)
            {
                Debug.Log(_nextTakeResourcesTime);
                Debug.Log(Time.time);
                if (_nextTakeResourcesTime <=Time.time)
                {
                    Debug.Log("HARVEST" );
                    _nextTakeResourcesTime = Time.time + (1f / _resourcesPerSecond);
                    _collector.TakeFromTarget();
                
                }
            }
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}
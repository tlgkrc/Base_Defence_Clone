using UnityEngine;
using UnityEngine.AI;

namespace ExampleAIStateMachine.Scripts
{
    internal class ReturnToStockPile:IAIState
    {
        #region Self Variables

        #region Private Variables

        private readonly Collector _collector;
        private readonly NavMeshAgent _navMeshAgent;

        #endregion

        #endregion

        public ReturnToStockPile(Collector collector,NavMeshAgent navMeshAgent)
        {
            _collector = collector;
            _navMeshAgent = navMeshAgent;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            _collector.StocksPile = Object.FindObjectOfType<StocksPile>();
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_collector.StocksPile.transform.position);
        
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
        }
    }
}
using UnityEngine;
using UnityEngine.AI;

namespace AI.States.Miner
{
    public class ReturnToGemStock: IAIStates
    {
        #region Self Variables

        #region PrivateVariables

        private readonly Subscribers.Miner _miner;
        private readonly NavMeshAgent _agent;
        private readonly Transform _gemStockTransform;

        #endregion

        #endregion

        public ReturnToGemStock(Subscribers.Miner miner,Transform gemStock,NavMeshAgent agent)
        {
            _miner = miner;
            _agent = agent;
            _gemStockTransform = gemStock;
        }

        public void Tick()
        {
            _agent.SetDestination(_gemStockTransform.position);
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            _miner.DropCollectedGem();
        }
    }
}
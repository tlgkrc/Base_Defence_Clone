using Enums;
using Enums.Animations;
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
        private readonly Animator _animator;

        #endregion

        #endregion

        public ReturnToGemStock(Subscribers.Miner miner,Transform gemStock,NavMeshAgent agent,Animator animator)
        {
            _miner = miner;
            _agent = agent;
            _gemStockTransform = gemStock;
            _animator = animator;
        }

        public void Tick()
        {
            _agent.SetDestination(_gemStockTransform.position);
        }

        public void OnEnter()
        {
            _animator.SetTrigger(GemWorkerAnimTypes.CarryingWalk.ToString());
        }

        public void OnExit()
        {
            _miner.HoldGem(false);
        }
    }
}
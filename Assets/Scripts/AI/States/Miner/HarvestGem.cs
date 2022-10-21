using UnityEngine;

namespace AI.States.Miner
{
    public class HarvestGem: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.Miner _miner;

        #endregion

        #endregion

        public HarvestGem(Subscribers.Miner miner)
        {
            _miner = miner;
        }
        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            _miner.HoldGem(true);
        }

        public void OnExit()
        {
            
        }
    }
}
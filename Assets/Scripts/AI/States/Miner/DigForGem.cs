using UnityEngine;

namespace AI.States.Miner
{
    public class DigForGem: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.Miner _miner;
        private float _nextTakeResourcesTime;
        

        #endregion

        #endregion
        public DigForGem(Subscribers.Miner miner)
        {
            _miner = miner;
        }
        public void Tick()
        {
            if (_miner.Target !=null)
            {
                if (_nextTakeResourcesTime <=Time.time)
                {
                    _nextTakeResourcesTime = Time.time ;
                    _miner.TakeFromMine();
                }
            }
        }

        public void OnEnter()
        {
            //set miner animation as digging
        }

        public void OnExit()
        {
            
        }
    }
}
namespace AI.States.Miner
{
    public class HarvestGem: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private Subscribers.Miner _miner;

        #endregion

        #endregion

        public HarvestGem(Subscribers.Miner miner)
        {
            _miner = miner;
        }
        public void Tick()
        {
            //setactive objectpooled gem
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}
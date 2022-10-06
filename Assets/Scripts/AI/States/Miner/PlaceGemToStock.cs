using Signals;

namespace AI.States.Miner
{
    public class PlaceGemToStock: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private Subscribers.Miner _miner;

        #endregion

        #endregion

        public PlaceGemToStock(Subscribers.Miner miner)
        {
            _miner = miner;
        }
        public void Tick()
        {
            StackSignals.Instance.onAddStack?.Invoke();
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}

namespace AI.States.Miner
{
    public class PlaceGemToStock: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.Miner _miner;

        #endregion

        #endregion

        public PlaceGemToStock(Subscribers.Miner miner)
        {
            _miner = miner;
        }
        public void Tick()
        {
            _miner.AddGemToStock();
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}
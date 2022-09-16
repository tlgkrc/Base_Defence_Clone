namespace ExampleAIStateMachine.Scripts
{
    internal class PlaceResourcesInStockPile:IAIState
    {
        #region Self Variables

        #region Private Variables

        private readonly Collector _collector;

        #endregion

        #endregion

        public PlaceResourcesInStockPile(Collector collector)
        {
            _collector = collector;
        }
        public void Tick()
        {
            if (_collector.Take())
            {
                _collector.StocksPile.Add();
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
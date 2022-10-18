namespace AI.States.MoneyCollector
{
    public class TakeMoney: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private Subscribers.MoneyCollector _moneyCollector;

        #endregion

        #endregion

        public TakeMoney(Subscribers.MoneyCollector moneyCollector)
        {
            _moneyCollector = moneyCollector;
        }
        public void Tick()
        {
            throw new System.NotImplementedException();
        }

        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}
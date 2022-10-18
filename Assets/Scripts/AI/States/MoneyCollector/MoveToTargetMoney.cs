namespace AI.States.MoneyCollector
{
    public class MoveToTargetMoney: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private Subscribers.MoneyCollector _moneyCollector;

        #endregion

        #endregion

        public MoveToTargetMoney(Subscribers.MoneyCollector moneyCollector)
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
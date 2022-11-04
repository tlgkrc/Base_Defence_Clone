
namespace AI.States.MoneyCollector
{
    public class DeliverMoney: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.MoneyCollector _moneyCollector;
        
        #endregion

        #endregion

        public DeliverMoney(Subscribers.MoneyCollector moneyCollector)
        {
            _moneyCollector = moneyCollector;
        }
        public void Tick()
        {
            _moneyCollector.DeliverMoney();
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
            _moneyCollector.ResetGrid();
        }
    }
}
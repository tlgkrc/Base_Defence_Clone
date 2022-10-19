using Signals;

namespace AI.States.MoneyCollector
{
    public class TakeMoney: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.MoneyCollector _moneyCollector;

        #endregion

        #endregion

        public TakeMoney(Subscribers.MoneyCollector moneyCollector)
        {
            _moneyCollector = moneyCollector;
        }
        public void Tick()
        {
        }

        public void OnEnter()
        {
            _moneyCollector.TakeMoneyToStack();
        }

        public void OnExit()
        {
        }
    }
}
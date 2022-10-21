﻿namespace AI.States.MoneyCollector
{
    public class SearchClosestMoney: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.MoneyCollector _moneyCollector;

        #endregion

        #endregion

        public SearchClosestMoney(Subscribers.MoneyCollector moneyCollector)
        {
            _moneyCollector = moneyCollector;
        }
        public void Tick()
        {
            _moneyCollector.SearchMoney();
        }

        public void OnEnter()
        {
            _moneyCollector.Target = null;
        }

        public void OnExit()
        {
            _moneyCollector.ResetRadius();
        }
    }
}
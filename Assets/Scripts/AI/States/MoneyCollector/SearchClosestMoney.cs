using Enums.Animations;
using UnityEngine;

namespace AI.States.MoneyCollector
{
    public class SearchClosestMoney: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.MoneyCollector _moneyCollector;
        private Animator _animator;

        #endregion

        #endregion

        public SearchClosestMoney(Subscribers.MoneyCollector moneyCollector,Animator animator)
        {
            _moneyCollector = moneyCollector;
            _animator = animator;
        }
        public void Tick()
        {
            _moneyCollector.SearchMoney();
        }

        public void OnEnter()
        {
            _moneyCollector.Target = null;
            _animator.SetTrigger(MoneyWorkerAnimTypes.Idle.ToString());
        }

        public void OnExit()
        {
            _moneyCollector.ResetRadius();
        }
    }
}
﻿namespace AI.States.Enemy
{
    public class SearchForPlayer: IAIStates
    {
        #region Self Variables

        #region Public Variables
        
        

        #endregion

        #region Private Variables

        private Subscribers.Enemy _enemy;

        #endregion

        #endregion

        public SearchForPlayer(Subscribers.Enemy enemy)
        {
            _enemy = enemy;
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
namespace AI.States.Enemy
{
    public class Attack: IAIStates
    {
        #region Self Variables

        #region Public Variables
        
        

        #endregion

        #region Private Variables

        private Subscribers.Enemy _enemy;

        #endregion

        #endregion

        public Attack(Subscribers.Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Tick()
        {

        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {

        }
    }
}
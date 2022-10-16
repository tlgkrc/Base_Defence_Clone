namespace AI.States.AmmoSupplier
{
    public class DeliverAmmoBoxes: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.AmmoSupplier _ammoSupplier;
        
        #endregion

        #endregion

        public DeliverAmmoBoxes(Subscribers.AmmoSupplier ammoSupplier)
        {
            _ammoSupplier = ammoSupplier;
        }
        public void Tick()
        {
            _ammoSupplier.DeliverAmmo(_ammoSupplier.TargetIndex);
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}
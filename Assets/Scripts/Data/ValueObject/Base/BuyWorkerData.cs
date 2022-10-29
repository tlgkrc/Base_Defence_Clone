using System;

namespace Data.ValueObject.Base
{
    [Serializable]
    public class BuyWorkerData
    {
        public int CostOfMoneyWorker;
        public int PaidAmountForMoneyWorker;
        public int CostOfAmmoWorker;
        public int PaidAmountForAmmoWorker;
        public float DelayTime;
    }
}
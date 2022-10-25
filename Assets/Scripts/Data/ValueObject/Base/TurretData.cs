using System;

namespace Data.ValueObject.Base
{
    [Serializable]
    public class TurretData
    {
        public int TurretCost;
        public bool TurretCostisPaid;
        public int TurretSoldierCost;
        public bool TurretSoldierCostPaid;
    }
}
using System;
using UnityEngine;

namespace Data.ValueObject.Base
{
    [Serializable]
    public class TurretGOData
    {
        public int TurretSoldierCost;
        public int TurretSoldierCostPaid;
        public Vector3 RotationBorderOffset;
        public float ShootingDelay;
        public float BuyDelay;
    }
}
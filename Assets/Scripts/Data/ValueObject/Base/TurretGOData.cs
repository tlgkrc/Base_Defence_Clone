using System;
using UnityEngine;

namespace Data.ValueObject.Base
{
    [Serializable]
    public class TurretGOData
    {
        public int TurretSoldierCost;
        public bool TurretSoldierCostPaid;
        public Vector3 RotationBorderOffset;
        public float ShootingDelay;
    }
}
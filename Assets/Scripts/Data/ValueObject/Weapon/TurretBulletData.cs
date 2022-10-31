using System;

namespace Data.ValueObject.Weapon
{
    [Serializable]
    public class TurretBulletData
    {
        public float LifeTime;
        public int ForceFactor;
        public int Damage;
    }
}
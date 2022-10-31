using System;

namespace Data.ValueObject.Weapon
{
    [Serializable]
    public class BulletGOData
    {
        public TurretBulletData TurretBulletData;
        public WeaponBulletData WeaponBulletData;
    }
}
using System;

namespace Data.ValueObject.Weapon
{
    [Serializable]
    public class GrenadeData
    {
        public int Damage;
        public int ExplosionForce;
        public float StopExplosionTime;
        public float ExplosionRadius;
    }
}
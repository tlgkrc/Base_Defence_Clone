using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class GunGOData
    {
        public GameObject Gun;
        public int Cost;
        public int Damage;
        public float BulletLifeTime;
        public int BulletSpeed;

    }
}
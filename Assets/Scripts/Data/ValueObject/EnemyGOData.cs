using System;
using UnityEditor.Animations;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyGOData
    {
        public int Damage;
        public float Speed;
        public float IncreasedSpeed;
    }
}
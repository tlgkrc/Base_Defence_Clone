using System;
using System.Numerics;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public PlayerMovementData MovementData;
        public int MaxStackCount;
    }

    [Serializable]
    public class PlayerMovementData
    {
        public float Speed;
        public float IdleRotateSpeed;
        
    }
}
using System;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public PlayerMovementData MovementData;
        public int MaxStackCount;
        public int Health;
    }

    [Serializable]
    public class PlayerMovementData
    {
        public float Speed;
        public float IdleRotateSpeed;
        

    }
}